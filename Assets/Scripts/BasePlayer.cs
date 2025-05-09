using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StatController;
using UnityEngine.Playables;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;

public class BasePlayer : MonoBehaviour
{
    // 충돌판정을 위한 rigidbody
    protected Rigidbody2D player_rigidbody;
    
    // 플레이어, 무기 스프라이트 출력방향을 위한 필드 선언
    [SerializeField] protected SpriteRenderer playerSprite;
    // [SerializeField] protected Weapon currentWeapon; 
    // 무기 스프라이트는 Weapon 클래스에서 받아오고 weaponPivot 오브젝트를 회전할 때 사용
  
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }
    // 캡슐화. 추후 Weapon 클래스에서 발사체 방향값 받아갈 때 사용
    protected Vector2 movementDirection = Vector2.zero;
    // public Vector2 MovemetDirection { get { return movementDirection; } }
    // 혹시 몬스터의 장판 패턴같은 게 유저의 이동방향을 고려한다면 필요할지도?
    // 넉벡 관련해서는 플레이어는 넉벡되지 않기로 했으니 weapon 클래스에서 만들어도?

    protected Animator player_Animator;
    protected StatController player_Stat;
    // 스탯변화 및 애니매이션을 넣어야 할 때 불러올 외부 인스턴스

    protected enum PLAYER_STATE
    {
    WAIT,
    IDLE,
    MOVE,
    DAMAGE,
    DIE
    }

    protected PLAYER_STATE current_STATE = PLAYER_STATE.WAIT;
    protected void Awake()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();
        player_Animator = GetComponent<Animator>();
        player_Stat = GetComponent<StatController>();
    }

    protected void Start()
    {
        // 체력, 최대체력, 공격력, 방어력, 이동속도, 골드, 크리확률, 크리배율, 무적시간, 무적여부
        // 추후 ScriptableObject로 구현하여 실수 줄이기
        player_Stat.InitStat(100, 100, 5, 1, 2f, 50, 0.1f, 1.5f, 0.3f, false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (current_STATE == PLAYER_STATE.DAMAGE)
            return; // 이미 데미지 중이면 무시
        
        GameObject attackSource = collision.gameObject;
        if (attackSource.CompareTag("Enemy")) // 적 태그가 붙어있는 경우
        {
            if (attackSource.TryGetComponent(out IDamageInfo damageinfo)) // 충돌한 게임오브젝트의 데미지 정보 인터페이스를 받으면
            {
                DamageResult result = player_Stat.FinalDamageCalculator(damageinfo); // 최종뎀 계산
                player_Stat.HpReductionApply(result); // 최종뎀 체력 적용
                // UIManager.ShowDamageUI(result.final_Damage, result.is_Critical); 데미지를 띄우는 UI 메서드
                // player_Animator.어쩌구 해서 애니메이션 파라미터 변경 (player_Animator.isHit = true 뭐 이런 식으로)
                // if(투사체) attackSource.SelfDestroy(); 투사체일 경우 투사체 삭제
                current_STATE = PLAYER_STATE.DAMAGE;
                StartCoroutine(ApplyInvincible());
            }
        }
    }

    protected IEnumerator ApplyInvincible()
    {
        yield return new WaitForSeconds(player_Stat.Invinsible_Duration);
        current_STATE = PLAYER_STATE.IDLE;
        //player_Animator.isInvinsible = false; 이런 식으로 무적시간 끝나면 깜빡이는 에니매이션 종료한다거나
    }




}
