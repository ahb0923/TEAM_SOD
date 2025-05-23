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
    DIE
    //DAMAGE, 피격 시 무적시간은 IDLE,MOVE 등 다른 상태와 동시에 가져야 하기 때문에 emum이 아닌 Stat에서 bool값으로 처리하기로 함
}
public class BasePlayer : MonoBehaviour
{
    // 충돌판정을 위한 rigidbody
    protected Rigidbody2D player_rigidbody;

    // 플레이어 출력방향을 위한 필드 선언
    [SerializeField] protected SpriteRenderer playerSprite;

    // 플레이어의 시점 및 트랜스폼 관련. 프로퍼티로 혹시 외부에서 필요할까봐 열어놨는데 아직까진 필요가 없네요
    protected Vector2 lookDirection = Vector2.zero;
    protected Transform myPosition;
    protected float distance_Between_Two;
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }
    public Transform MyPosition { get { return myPosition; } }
    public float Distance_Between_Two { get { return distance_Between_Two; } }
    // public Vector2 MovemetDirection { get { return movementDirection; } }
    

    // 스탯변화 및 애니매이션을 넣어야 할 때 필요한 외부 인스턴스들
    protected StatController player_Stat;
    protected Animator player_Animator;
    [SerializeField] protected DamageText player_DamageText;
    // 유저 상태값 초기화
    [SerializeField]
    private PLAYER_STATE currState = PLAYER_STATE.WAIT; // 별도 설정 없으면 WAIT 상태

    /*[SerializeField]
    private PLAYER_STATE nextState;*/

    //===============이하 추후 수정 필요 : 무기 및 적 탐지 관련===================
    // 추후 무기 데이터베이스 쪽에서 프리펩을 리스트로 불러오는 느낌? 지금은 임시로 직접 프리펩 등록
    protected RangeWeapon player_CurrentWeapon;
    //protected GameObject weaponprefabs;
    
    // 적 탐지 관련 : 던전에서 진행 or 유저가 탐색? 참고:몬스터 쪽은 플레이어를 직접 탐색 중.
    //protected GameObject[] enemyArray;
    //protected List<Transform> targetPosition;
    //protected int monsterCount = 0;

    [SerializeField] private LayerMask enemyLayer = 1 << 9;



    protected void Awake()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();
        player_Animator = GetComponentInChildren<Animator>();
        player_Stat = GetComponent<StatController>();
        myPosition = GetComponent<Transform>();
        //GetClosestEnemyFromPlayer();
        //targetPosition = new List<Transform>();
        //MakeEnemyArray();
    }
    

    protected void Start()
    {
        player_Stat.InitStat_Player();
    }

    public void PlayerWeaponSelect(WeaponData selected_Weapon)
    // 추후 로비에서 무기 고르는 느낌으로 가야 할 듯? 로비신 구성되고 무기 리스트로 관리되고 npc 추가되면 수정
    {
        if (player_CurrentWeapon != null)
        {
            Destroy(player_CurrentWeapon.gameObject);
        }
        GameObject weapon_Instance = Instantiate(selected_Weapon.prefab, this.transform);

        if (weapon_Instance != null) Debug.Log("웨폰 인스탄스 불러오기 성공");
        player_CurrentWeapon = weapon_Instance.GetComponent<RangeWeapon>();
        if (player_CurrentWeapon != null) Debug.Log("잘 불러왔음");
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
        if (player_CurrentWeapon != null)
        {
            GetClosestEnemyFromPlayer();
            player_CurrentWeapon.FaceTarget(lookDirection);
        }
        if (player_CurrentWeapon == null) lookDirection = movementDirection;
        if (currState != PLAYER_STATE.WAIT || currState != PLAYER_STATE.DIE)
        // 축거나 대기상태일 경우 공격하지 않도록 (죽었을 경우의 액션에 대해 추가기획 필요)
        {

            if (player_CurrentWeapon != null) AttackOrNot();
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


  


    protected void AttackOrNot()
    {
        if (currState == PLAYER_STATE.IDLE && distance_Between_Two <= player_CurrentWeapon.AttackRange)
        {
            player_CurrentWeapon.Attack(lookDirection);
        }
    }


    protected void Rotate(Vector2 look_Direction)
    {
        
        Vector2 localLookDirection = look_Direction - (Vector2)this.transform.position;
        if (look_Direction == Vector2.zero) return; // 무효한 방향 방지
        bool isLeft = localLookDirection.x < 0f;
        
        playerSprite.flipX = isLeft;


    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("콜리전에서 충돌 인식함");
        GameObject attackSource = collision.gameObject;
        Debug.Log($"콜리전에서 충돌 인식한 어택소스의 레이어 : {attackSource.layer}");
        if (attackSource.layer == LayerMask.NameToLayer("Background")) return;
        // 백그라운드 충돌 시에는 리턴. 현재 설정에서 나머지 충돌할 레이어가 적투사체밖에 없음
        
        if (attackSource.TryGetComponent<ProjectileController>(out ProjectileController proj))
        {
            Debug.Log("원거리에 맞음");
            if (player_Stat.Is_Invinsible) // 무적 중이면 데미지 처리 X
            {
                proj.DestroyProjectile(attackSource.transform.position);
                return;
            }
            float atk = proj.GetAttackPower();
            float crit_C = proj.GetCriChance();
            float crit_M = proj.GetCriMutiply();
            DamageResult result = player_Stat.FinalDamageCalculator(atk, crit_C, crit_M);
            player_Stat.HpReductionApply(result); // 최종뎀 체력 적용
            proj.DestroyProjectile(attackSource.transform.position); // 퍼블릭으로 열어주세요
            player_DamageText.SetDamage((int)result.final_Damage); // 데미지 출력
            player_DamageText.gameObject.SetActive(true);
            Debug.Log($"플레이어 체력 : {player_Stat.Hp}");
            StartCoroutine(ApplyInvincible()); // 무적 적용 코루틴 실행
        }
       // if (attackSource.CompareTag("Monster_MeleeWeapon"))
       // {
       //     Debug.Log("콜리전에서 근접에 맞음");
       //     MeleeWeapon meleeWeapon = attackSource.GetComponent<MeleeWeapon>();
       //     if (player_Stat.Is_Invinsible) // 무적 중이면 데미지 처리 X
       //     {
       //         return;
       //     }
       //     float atk = meleeWeapon.GetAttackPower();
       //     float crit_C = meleeWeapon.GetCriChance();
       //     float crit_M = meleeWeapon.GetCriMutiply();
       //     DamageResult result = player_Stat.FinalDamageCalculator(atk, crit_C, crit_M);
       //     player_Stat.HpReductionApply(result); // 최종뎀 체력 적용
       //     player_DamageText.SetDamage((int)result.final_Damage); // 데미지 출력
       //     player_DamageText.gameObject.SetActive(true);
       //     Debug.Log($"플레이어 체력 : {player_Stat.Hp}");
       //     StartCoroutine(ApplyInvincible()); // 무적 적용 코루틴 실행
       // }
    }

   protected void OnTriggerStay2D(Collider2D collision)
   {
       Debug.Log("트리거에서 충돌 인식함");
       GameObject attackSource = collision.gameObject;
       Debug.Log($"트리거에서 충돌 인식한 어택소스의 레이어 : {attackSource.layer}");
       if (attackSource.layer == LayerMask.NameToLayer("Background")) return;
        // 백그라운드 충돌 시에는 리턴. 현재 설정에서 나머지 충돌할 레이어가 적투사체밖에 없음
        if (attackSource.CompareTag("Monster_MeleeWeapon"))
        {
            Debug.Log("트리거에서 근접에 맞음");
            MeleeWeapon meleeWeapon = attackSource.GetComponentInParent<MeleeWeapon>();
            if (player_Stat.Is_Invinsible) // 무적 중이면 데미지 처리 X
            {
                return;
            }
            float atk = meleeWeapon.GetAttackPower();
            float crit_C = meleeWeapon.GetCriChance();
            float crit_M = meleeWeapon.GetCriMutiply();
            DamageResult result = player_Stat.FinalDamageCalculator(atk, crit_C, crit_M);
            player_Stat.HpReductionApply(result); // 최종뎀 체력 적용
            player_DamageText.SetDamage((int)result.final_Damage); // 데미지 출력
            player_DamageText.gameObject.SetActive(true);
            Debug.Log($"플레이어 체력 : {player_Stat.Hp}");
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
    // 가장 가까운 적을 찾는 로직
    // targetPosition[] foreach문을 돌려서 유저와의 거리를 계산해서 가장 짧은 위치로의 벡터값을
    // lookDirection에 저장
    // Physics2D.OverlapCircleAll을 이용할 수도 있음
    // protected void FindClosestEmemy()
    // {
    //     if (targetPosition == null || targetPosition.Count == 0)
    //     {
    //         lookDirection = Vector2.right;
    //         distance_Between_Two = float.MaxValue;
    //         return;
    //     }
    //     // 추적할 적이 없다면 종료 : 적을 다 잡았거나 애초에 적이 없는 방일 경우(로비) NullRef 방지. 기본 방향은 오른쪽 보게 하기.
    //     Transform nearest = null;
    //     distance_Between_Two = float.MaxValue;
    //
    //     // 이번 업데이트 사이클의 targetPosition 배열의 숫자와 기존의 enemyArray 수가 다르다면 enemyArray 재작성
    //     if (monsterCount != enemyArray.Count())
    //         MakeEnemyArray();
    //
    //     monsterCount = 0;
    //     ClearDeadEnemyOnArray();
    //     foreach (Transform enemyTransform in targetPosition)
    //     {
    //         monsterCount++;
    //         float distance = Vector2.Distance(myPosition.position, enemyTransform.position);
    //         if (distance < distance_Between_Two)
    //         {
    //             distance_Between_Two = distance;
    //             nearest = enemyTransform;
    //         }
    //     }
    //
    //     if (nearest != null)
    //     {
    //         lookDirection = nearest.position;
    //     }
    //     else
    //     {
    //         lookDirection = Vector2.right; //기본 방향은 오른쪽 보게 하기.
    //     }
    //
    //
    //
    // }

    //『효빈』 => 이것도 던전매니저에서 다뤄주면 좋을것 같습니다.
    //  protected void ClearDeadEnemyOnArray()
    //  {
    //      if (targetPosition == null)
    //      {
    //          // 추적할 적이 없다면 종료 : 적을 다 잡았거나 애초에 적이 없는 방일 경우(로비) NullRef 방지
    //          return;
    //      }
    //      targetPosition = targetPosition.Where(enemy => enemy != null).ToList();
    //      // 죽은 적을 리스트에서 제거해야 함. 그래야 추적에서 적 죽은 곳에 계속 때리지 않음
    //
    //      if (targetPosition.Count == 0)
    //      {
    //          // 웨이브 클리어
    //          //Debug.Log("몬스터를 찾을 수 없습니다.");
    //      }
    //  }
   // protected void MakeEnemyArray()
   // {
   //     // 『효빈』 추후 던전 매니저에서 관리하도록 수정[던전매니저 내부에 Enemy 리스트가 존재] 인스턴스를 통해서 받아와 사용하면 될거 같습니다.
   //     //  DungeonManager에서 MonsterFactory를 만들어 거기에서 몬스터를 생성 및 관리하게 하는게 좋을 것 같습니다.
   //     enemyArray = FindObjectsOfType<Monster>()
   //                 .Select(monster => monster.gameObject)
   //                 .ToArray(); // 씬 내부에서 Monster 컴포넌트를 가진 게임오브젝트를 어레이로 저장
   //     for (int i = 0; i < enemyArray.Length; i++)
   //     {
   //         targetPosition.Add(enemyArray[i].transform); // 게임오브젝트의 트랜스폼 정보 저장
   //     }
   //     Debug.Log(enemyArray.Count());
   // }
    public float ReturnPlayerHP()
        // 유저의 HP값을 UI에 띄워야 할 때 쓰세용. 근데 UI에 어떤 정보를 띄울지 정해지면 이따가 마무리되면 얘기해봐용
    {
        return player_Stat.Hp;
    }


    private void GetClosestEnemyFromPlayer()
    {
        Collider2D[] enemysInRange = Physics2D.OverlapCircleAll(transform.position, player_CurrentWeapon.AttackRange, enemyLayer);

        if (enemysInRange.Length == 0)
        {
            lookDirection = movementDirection;
            distance_Between_Two = float.MaxValue;
            Debug.Log("적 현재 없음");
            return;
        }
            

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider2D hit in enemysInRange)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = hit.transform;
            }
        }

        distance_Between_Two = closestDistance;
        lookDirection = closestEnemy.position;
        //Debug.Log($"가장 가까운 적의 위치 : {lookDirection}");
        //Debug.Log($"가장 가까운 적과의 거리 : {distance_Between_Two}");
    }

}
