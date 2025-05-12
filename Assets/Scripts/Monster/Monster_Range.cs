using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Range : MonoBehaviour
{
    // ������ �ӽ÷� ����
    private float _hp = 10;
    private float _maxHp = 10;
    private float _atk = 10;
    private float _def = 10;
    private float _moveSpeed = 100f;
    private int _gold = 100;
    private float _crit_Chance = 0;
    private float _crit_Multiply = 0;
    private bool _is_invinsible;
    private float _invinsible_duration = 0;

    private GameObject target;
    [SerializeField] Transform weaponPivot;
    public BaseWeapon weaponPrefab;
    protected BaseWeapon weapon;


    StatController monsterStat;
    [SerializeField] private float _checkRange;  // Ÿ�� Ž�� ����
    [SerializeField] private float _attackRange; // ���� ��Ÿ�
    [SerializeField] private float _attackDelay; // ���� �ֱ�
    private float delay; // ���� ������ ���� ����
    private float knockPower; // �˹� ��ġ
    private bool isDamage; // �ǰ�
    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("Player"); // �÷��̾� �̸��� ���� ���氡��
        //monsterStat.InitStat(_hp, _maxHp, _atk, _def, _moveSpeed, _gold, _crit_Chance, _crit_Multiply, _invinsible_duration, _is_invinsible);
        if (weapon != null)
        {
            weapon = Instantiate(weaponPrefab, weaponPivot);
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    }

    public void Move()
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) > _checkRange)
        {
            anim.SetBool("IsRun", false);
            rigid.velocity = Vector2.zero;
            return;
        }
        else if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) <= _attackRange)
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
            }
            if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            rigid.velocity = direction * _moveSpeed * Time.deltaTime;
            anim.SetBool("IsRun", true);
        }
    }
    public void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _attackRange && delay >= _attackDelay)
        {
            //����
            CreateProjectile();
            Debug.Log("���Ÿ� ����");
            delay = 0;
        }
        else
        {
            delay += Time.deltaTime;
        }

    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectile")) //���̾� �̸� ���� �÷��̾� �Ѿ˰� �浹������
        {
            GameObject attackSource = collision.gameObject;
            if (attackSource.TryGetComponent(out IDamageInfo damageinfo))
            {
                StatController.DamageResult result = monsterStat.FinalDamageCalculator(damageinfo);
                monsterStat.HpReductionApply(result);
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
    }*/


    IEnumerator DamageCheck() // ���� ������ ������ ����Ұ�� ���, �ִϸ��̼� ����, �˹��߿��� �˹��� �ȵǰ� ����
    {
        isDamage = true;
        anim.SetTrigger("IsDamage");
        yield return new WaitForSeconds(0.5f);
        isDamage = false;
    }
    private void CreateProjectile()
    {
        //ź�� ���� �� ����     
        Vector2 direction = (target.transform.position - transform.position).normalized;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void KnockBack(Vector2 collision)
    {
        Vector2 dir = new Vector2(collision.x - transform.position.x, collision.y - transform.position.y).normalized;
        rigid.AddForce(dir * knockPower, ForceMode2D.Impulse);
    }
}
