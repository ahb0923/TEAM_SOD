using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


enum MONSTER_KEY
{
    Melee_Test,
    Range_Test,
}
public class Monster : MonoBehaviour
{
    public GameObject target;
    [SerializeField] protected GameObject weaponPivot;
    [SerializeField] protected StatController monsterStat;
    [SerializeField] protected float _checkRange;  // 타겟 탐색 범위
    [SerializeField] protected float _attackRange; // 공격 사거리
    [SerializeField] protected float _attackDelay; // 공격 주기
    [SerializeField] public DamageText[] dmgText;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected ParticleSystem particle;
    protected float delay; // 공격 딜레이 계산용 변수
    protected float knockPower; // 넉백 수치
    protected bool isDamage; // 피격
    protected Rigidbody2D rigid;
    protected Animator anim;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        monsterStat.InitStat_Monster();
        if (target == null)
        {
            target = GameObject.Find("Player");
        }
    }

    protected void OnEnable()
    {
        if(particle == null)
        {
            return;
        }
        particle.Play();
    }
    protected virtual void Start() { }
    protected virtual void Update() 
    {
        delay += Time.deltaTime;
        Attack();
        //MonsterRotate();
        Move();
    }

    protected virtual void Move()
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) > _checkRange)
        {
            anim.SetBool("IsRun", false);
            rigid.velocity = Vector2.zero;
            return;
        }
        if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) <= _attackRange)
        {
            anim.SetBool("IsRun", false);
            rigid.velocity = Vector2.zero;
            return;
        }
        else
        {
            Vector2 direction = (target.transform.position - transform.position).normalized;
            if (direction.x > 0)
            {
                sprite.flipX = false;
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                weaponPivot.transform.localPosition = new Vector2(0.5f, 0);
                //weaponPivot.transform.rotation = Quaternion.Euler(0, 0, -90); 
            }
            else if (direction.x < 0)
            {
                sprite.flipX = true;
                //transform.rotation = Quaternion.Euler(0, 180, 0);
                weaponPivot.transform.localPosition = new Vector2(-0.5f, 0);
                //weaponPivot.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            rigid.velocity = direction * monsterStat.MoveSpeed * Time.deltaTime;
            anim.SetBool("IsRun", true);
        }
    }

    protected virtual void MonsterRotate()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponPivot.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            weaponPivot.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    protected virtual void Attack() { }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) //태그 예시 플레이어 총알과 충돌했을때
        {
            GameObject attackSource = collision.gameObject;
            if (attackSource.TryGetComponent<ProjectileController>(out ProjectileController proj))
            {
                float crit_C = proj.GetCriChance();
                float crit_M = proj.GetCriMutiply();
                float atk = proj.GetAttackPower(); // 최종 공격력을 리턴해주는 메서드 하나 있으면 될 듯?
                DamageResult result = monsterStat.FinalDamageCalculator(atk, crit_C, crit_M);
                monsterStat.HpReductionApply(result);
                Debug.Log(monsterStat.Hp);
                //dmgText.gameObject.SetActive(true);
                //dmgText.SetDamage((int)result.final_Damage);
                ShowDamageText(result.final_Damage);
                proj.DestroyProjectile(proj.transform.position);
                anim.SetTrigger("IsDamage");
                // 최종뎀 계산
               /* if (!isDamage)
                {
                    KnockBack(collision.transform.position);
                    StartCoroutine("DamageCheck");
                }*/
                if (monsterStat.Hp <= 0)// 체력 0이하 일시 Death() 함수 호출
                {
                    Death();
                }
            }
        }
    }
    protected virtual IEnumerator DamageCheck() // 몬스터 데미지 애니메이션 및 (데미지 연산 중 무적 적용시 사용) < 선택
    {
        isDamage = true;
        anim.SetTrigger("IsDamage");
        yield return new WaitForSeconds(0.5f);
        isDamage = false;
    }
    public virtual void Death()
    {
        
    }

    public virtual void KnockBack(Vector2 collision)
    {
        Vector2 dir = new Vector2(collision.x - transform.position.x, collision.y - transform.position.y).normalized;
        rigid.AddForce(dir * knockPower, ForceMode2D.Impulse);
    }

    public virtual void CheckPlayer() // 플레이어를 타겟으로 찾을 때 필요하면 사용
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _checkRange, LayerMask.GetMask("Player"));
        if(hit != null)
        {
            target = hit.gameObject;
        }
    }

    public virtual void ShowDamageText(float damage)
    {
        for(int i = 0; i < dmgText.Length; i++)
        {
            if (!dmgText[i].gameObject.activeSelf)
            {
                dmgText[i].gameObject.SetActive(true);
                dmgText[i].SetDamage((int)damage);
                return;
            }
        }
    }

}
