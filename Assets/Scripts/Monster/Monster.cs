using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Monster : MonoBehaviour
{
    //[SerializeField] MonsterData_ho data;

    // 스탯은 임시로 적용
    protected float _hp = 10;
    protected float _maxHp = 10;
    protected float _atk = 10;
    protected float _def = 10;
    protected float _moveSpeed = 100f;
    protected int _gold = 100;
    protected float _crit_Chance = 0;
    protected float _crit_Multiply = 0;
    protected bool _is_invinsible;
    protected float _invinsible_duration = 0;

    public GameObject target;
    [SerializeField] protected GameObject weaponPivot;
    [SerializeField] protected StatController monsterStat;
    [SerializeField] protected float _checkRange;  // 타겟 탐색 범위
    [SerializeField] protected float _attackRange; // 공격 사거리
    [SerializeField] protected float _attackDelay; // 공격 주기
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

    protected virtual void Start() { }
    protected virtual void Update() 
    {
        delay += Time.deltaTime;
        Attack();
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
                transform.rotation = Quaternion.Euler(0, 0, 0);
                weaponPivot.transform.rotation = Quaternion.Euler(0, 0, -90);              
            }
            else if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                weaponPivot.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            rigid.velocity = direction * _moveSpeed * Time.deltaTime;
            anim.SetBool("IsRun", true);
        }
    }

    protected virtual void Attack() { }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) //태그 예시 플레이어 총알과 충돌했을때
        {
            Debug.Log("맞음");
            GameObject attackSource = collision.gameObject;
            if (attackSource.TryGetComponent<ProjectileController>(out ProjectileController proj))
            {
                float atk = proj.GetAttackPower(); // 최종 공격력을 리턴해주는 메서드 하나 있으면 될 듯?
                DamageResult result = monsterStat.FinalDamageCalculator(atk);
                monsterStat.HpReductionApply(result);
                Debug.Log(monsterStat.Hp);
                proj.DestroyProjectile(proj.transform.position);
                // 최종뎀 계산
                if (!isDamage)
                {
                    KnockBack(collision.transform.position);
                    StartCoroutine("DamageCheck");
                }
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
        Destroy(gameObject);
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

}
