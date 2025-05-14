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
    [SerializeField] public DamageText[] dmgText;
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected ParticleSystem particle;
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) //�±� ���� �÷��̾� �Ѿ˰� �浹������
        {
            GameObject attackSource = collision.gameObject;
            if (attackSource.TryGetComponent<ProjectileController>(out ProjectileController proj))
            {
                float crit_C = proj.GetCriChance();
                float crit_M = proj.GetCriMutiply();
                float atk = proj.GetAttackPower(); // ���� ���ݷ��� �������ִ� �޼��� �ϳ� ������ �� ��?
                DamageResult result = monsterStat.FinalDamageCalculator(atk, crit_C, crit_M);
                monsterStat.HpReductionApply(result);
                Debug.Log(monsterStat.Hp);
                //dmgText.gameObject.SetActive(true);
                //dmgText.SetDamage((int)result.final_Damage);
                ShowDamageText(result.final_Damage);
                proj.DestroyProjectile(proj.transform.position);
                anim.SetTrigger("IsDamage");
                // ������ ���
               /* if (!isDamage)
                {
                    KnockBack(collision.transform.position);
                    StartCoroutine("DamageCheck");
                }*/
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
