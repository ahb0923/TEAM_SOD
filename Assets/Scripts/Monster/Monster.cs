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

    // ������ �ӽ÷� ����
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
    [SerializeField] protected float _checkRange;  // Ÿ�� Ž�� ����
    [SerializeField] protected float _attackRange; // ���� ��Ÿ�
    [SerializeField] protected float _attackDelay; // ���� �ֱ�
    protected float delay; // ���� ������ ���� ����
    protected float knockPower; // �˹� ��ġ
    protected bool isDamage; // �ǰ�
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) //�±� ���� �÷��̾� �Ѿ˰� �浹������
        {
            Debug.Log("����");
            GameObject attackSource = collision.gameObject;
            if (attackSource.TryGetComponent<ProjectileController>(out ProjectileController proj))
            {
                float atk = proj.GetAttackPower(); // ���� ���ݷ��� �������ִ� �޼��� �ϳ� ������ �� ��?
                DamageResult result = monsterStat.FinalDamageCalculator(atk);
                monsterStat.HpReductionApply(result);
                Debug.Log(monsterStat.Hp);
                proj.DestroyProjectile(proj.transform.position);
                // ������ ���
                if (!isDamage)
                {
                    KnockBack(collision.transform.position);
                    StartCoroutine("DamageCheck");
                }
                if (monsterStat.Hp <= 0)// ü�� 0���� �Ͻ� Death() �Լ� ȣ��
                {
                    Death();
                }
            }
        }
    }
    protected virtual IEnumerator DamageCheck() // ���� ������ �ִϸ��̼� �� (������ ���� �� ���� ����� ���) < ����
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

    public virtual void CheckPlayer() // �÷��̾ Ÿ������ ã�� �� �ʿ��ϸ� ���
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, _checkRange, LayerMask.GetMask("Player"));
        if(hit != null)
        {
            target = hit.gameObject;
        }
    }

}
