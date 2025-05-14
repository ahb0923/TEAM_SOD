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
    [SerializeField] protected float _checkRange;  // Ÿ�� Ž�� ����
    [SerializeField] protected float _attackRange; // ���� ��Ÿ�
    [SerializeField] protected float _attackDelay; // ���� �ֱ�
    [SerializeField] protected DamageText dmgText;
    [SerializeField] protected SpriteRenderer sprite;
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
        MonsterRotate();
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
            rigid.velocity = direction * monsterStat.MoveSpeed* Time.deltaTime;
            Debug.Log(monsterStat.MoveSpeed + "�Դϴ�");
            Debug.Log(_attackRange + "�Դϴ�");
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) //�±� ���� �÷��̾� �Ѿ˰� �浹������
        {
            GameObject attackSource = collision.gameObject;
            if (attackSource.TryGetComponent<ProjectileController>(out ProjectileController proj))
            {
                float atk = proj.GetAttackPower(); // ���� ���ݷ��� �������ִ� �޼��� �ϳ� ������ �� ��?
                DamageResult result = monsterStat.FinalDamageCalculator(atk);
                monsterStat.HpReductionApply(result);
                dmgText.SetDamage((int)result.final_Damage);
                dmgText.gameObject.SetActive(true);
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
        PoolManager.Instance.ReturnObject("Melee_Test", this.gameObject);
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
