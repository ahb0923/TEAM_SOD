using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;
using System.Linq;
using UnityEngine.InputSystem;
using System;

public enum PLAYER_STATE
{
    NONE,
    WAIT,
    IDLE,
    MOVE,
    //DAMAGE, 피격 시 무적시간은 IDLE,MOVE 등 다른 상태와 동시에 가져야 하기 때문에 emum이 아닌 Stat에서 bool값으로 처리하기로 함
    DIE
}
public class BasePlayer : MonoBehaviour
{
    // 충돌판정을 위한 rigidbody
    protected Rigidbody2D player_rigidbody;

    // 플레이어, 무기 스프라이트 출력방향을 위한 필드 선언
    [SerializeField] protected SpriteRenderer playerSprite;

    [SerializeField] protected RangeWeapon currentWeapon;
    [SerializeField] protected SpriteRenderer weaponSprite;
    // 무기 스프라이트는 Weapon 클래스에서 받아오고 weaponPivot 오브젝트를 회전할 때 사용

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    protected Transform myPosition;
    public Transform MyPosition { get { return myPosition; } }

    protected float distance_Between_Two;
    public float Distance_Between_Two { get { return distance_Between_Two; } }
    // 추후 Weapon 클래스에서 발사체 방향값, 플레이어 위치값 받아갈 때 사용! 여기서 가져가심 됩니다 현오님

    protected Vector2 movementDirection = Vector2.zero;
    // public Vector2 MovemetDirection { get { return movementDirection; } }
    // 혹시 몬스터의 장판 패턴같은 게 유저의 이동방향을 고려한다면 필요할지도?


    protected GameObject[] enemyArray;
    protected List<Transform> targetPosition;
    protected StatController player_Stat;
    protected Animator player_Animator;
    // 스탯변화 및 애니매이션을 넣어야 할 때 불러올 외부 인스턴스

    [SerializeField]
    private PLAYER_STATE currState; // 별도 설정 없으면 WAIT 상태

    /*[SerializeField]
    private PLAYER_STATE nextState;*/

    protected void Awake()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();
        player_Animator = GetComponent<Animator>(); // 애니메이션 추가 시 구현
        player_Stat = GetComponent<StatController>();
        myPosition = GetComponent<Transform>();
        //currentWeapon = GetComponent<RangeWeapon>(); // 추후 무기를 불러오는 로직 추가 필요 (로비에서 무기를 고르는 형식)
        targetPosition = new List<Transform>();



        // 『효빈』 추후 던전 매니저에서 관리하도록 수정[던전매니저 내부에 Enemy 리스트가 존재] 인스턴스를 통해서 받아와 사용하면 될거 같습니다.
        //  DungeonManager에서 MonsterFactory를 만들어 거기에서 몬스터를 생성 및 관리하게 하는게 좋을 것 같습니다.
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy"); // 씬 내부에서 태그가 Enemy인 게임오브젝트 어레이로 저장
        for (int i = 0; i < enemyArray.Length; i++)
        {
            targetPosition[i] = enemyArray[i].transform; // 게임오브젝트의 트랜스폼 정보 저장
        }

        //『효빈』 state 초기화
        currState = PLAYER_STATE.IDLE;
        //nextState = PLAYER_STATE.IDLE;
    }



    // 체력, 최대체력, 공격력, 방어력, 이동속도, 골드, 크리확률, 크리배율, 무적시간, 무적여부
    // 추후 ScriptableObject로 구현하여 실수 줄이기
    protected void Start()
    {
        player_Stat.InitStat();
    }



    protected void Movement(Vector2 direction) // 키보드 이동 방향 구현. fixedUpdate에 물려서 호출
    {
        /*
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        player_rigidbody.velocity = movementDirection * player_Stat.MoveSpeed;*/

        // 『효빈』강의 막바지에 나왔던 InputAction을 활용해보았습니다 :)
        // moveSpeed = 5;
        direction = direction * player_Stat.MoveSpeed;
        bool movingLeft = direction.x < 0;
        player_rigidbody.velocity = direction;
    }

    //『효빈』 InputAction의 OnMove 함수
    private void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>().normalized;
        if (movementDirection.magnitude > 0) player_Animator.SetBool("IsMove",true);
        else player_Animator.SetBool("IsMove",false);
    }

    protected void FixedUpdate()
    {
        /*
        if(currState != PLAYER_STATE.WAIT)
            Move();
        // 유저가 보상 받는 동안 혹은 방 넘어가는 로딩 중에는 멈춰야 하니까.
        */

        DeterminePlayerSTATE();
        Rotate(lookDirection);
        Movement(movementDirection);
    }

    protected void Update()
    {
        if (currState != PLAYER_STATE.WAIT)
        // 위랑 마찬가지로 대기상태 중에는 아래의 행위들을 할 필요가 없지 않나요?
        {
            ClearDeadEnemyOnArray();
            FindClosestEmemy();
            AttackOrNot();
        }
    }
    // FixedUpdate랑 Update 어느 쪽에 뭘 넣으면 좋을지 순서에 대한 문제가 있는 것 같아요.

    public void SET_PLAYER_STATE(PLAYER_STATE state)
    {
        // 플레이어가 웨이브 클리어 보상을 고를 때 나오는 UI의 OnEnable/Disable에서 호출
        // SET_PLAYER_STATE(PLAYER_STATE.WAIT); 이런 식으로
        // 이 메서드 없이 그냥 바로 호출하려면 currState를 public으로 열면 되긴 합니다
        currState = state;

    }

    // 『효빈』FixedUpdate로 업데이트 위치 변경하였습니다.
    protected void DeterminePlayerSTATE()
    {
        if (currState == PLAYER_STATE.WAIT) return; // Wait이면 아래의 조건문 타지 않고 리턴
        // WAIT 상태를 벗어나려면 강제로 변환해 주어야 함! (위의 메서드를 이용하거나 그냥 값 변환)
        if (player_Stat.Hp <= 0) currState = PLAYER_STATE.DIE; // 사망판정 후

        else if (movementDirection == Vector2.zero) currState = PLAYER_STATE.IDLE; // 이동판정 후

        else if (movementDirection != Vector2.zero) currState = PLAYER_STATE.MOVE; // 다 아니면 IDLE

    }


    // 가장 가까운 적을 찾는 로직
    // targetPosition[] foreach문을 돌려서 유저와의 거리를 계산해서 가장 짧은 위치로의 벡터값을
    // lookDirection에 저장
    // Physics2D.OverlapCircleAll을 이용할 수도 있음
    protected void FindClosestEmemy()
    {
        if (targetPosition == null || targetPosition.Count == 0)
        {
            lookDirection = Vector2.right;
            distance_Between_Two = float.MaxValue;
            return;
        }
        // 추적할 적이 없다면 종료 : 적을 다 잡았거나 애초에 적이 없는 방일 경우(로비) NullRef 방지. 기본 방향은 오른쪽 보게 하기.
        Transform nearest = null;
        distance_Between_Two = float.MaxValue;

        foreach (Transform enemyTransform in targetPosition)
        {
            float distance = Vector2.Distance(myPosition.position, enemyTransform.position);
            if (distance < distance_Between_Two)
            {
                distance_Between_Two = distance;
                nearest = enemyTransform;
            }
        }

        if (nearest != null)
        {
            lookDirection = nearest.position;
        }
        else
        {
            lookDirection = Vector2.right; //기본 방향은 오른쪽 보게 하기.
        }
    }


    protected void AttackOrNot()
    {
        if (currState == PLAYER_STATE.IDLE && distance_Between_Two <= currentWeapon.AttackRange)
        {
            currentWeapon.Attack(lookDirection);
        }
    }


    protected void Rotate(Vector2 look_Direction)
    {
        float rotZ = Mathf.Atan2(look_Direction.y, look_Direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        playerSprite.flipX = isLeft;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject attackSource = collision.gameObject;
        if (attackSource.layer == 6) return; // 6번이 배경(Environments)라는 가정. 이따 머지작업 끝나면 한번에 레이어 추가해서 수정하죠?
        if (player_Stat.Is_Invinsible) return; // 이미 데미지 중이면 무시

        if (attackSource.TryGetComponent<ProjectileController>(out ProjectileController proj))
        {
            float atk = proj.GetAttackPower(); // 최종 공격력을 리턴해주는 메서드 하나 있으면 될 듯?
            DamageResult result = player_Stat.FinalDamageCalculator(atk); // 최종뎀 계산
            player_Stat.HpReductionApply(result); // 최종뎀 체력 적용
            proj.DestroyProjectile(attackSource.transform.position); // 퍼블릭으로 열어주세요

            //UIManager.ShowDamageUI(result.final_Damage, result.is_Critical); 데미지를 띄우는 UI 메서드
            StartCoroutine(ApplyInvincible()); // 무적 적용 코루틴 실행
        }
    }

    protected IEnumerator ApplyInvincible()
    {
        player_Stat.Is_Invincible_ChangeApply(true);
        player_Animator.SetBool("IsHit", true);
        yield return new WaitForSeconds(player_Stat.Invinsible_Duration);
        player_Stat.Is_Invincible_ChangeApply(false);
        player_Animator.SetBool("IsHit", false);
    }


    //==================================================================================================================================


    //『효빈』 => 이것도 던전매니저에서 다뤄주면 좋을것 같습니다.
    protected void ClearDeadEnemyOnArray()
    {
        if (targetPosition == null || targetPosition.Count == 0)
        {
            // 추적할 적이 없다면 종료 : 적을 다 잡았거나 애초에 적이 없는 방일 경우(로비) NullRef 방지
            return;
        }
        targetPosition = targetPosition.Where(enemy => enemy != null).ToList();
        // 죽은 적을 리스트에서 제거해야 함. 그래야 추적에서 적 죽은 곳에 계속 때리지 않음
    }

}
