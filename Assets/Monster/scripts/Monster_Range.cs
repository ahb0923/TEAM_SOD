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
    private float _in_invinsible_duration = 0;

    private GameObject target;

    public GameObject projectile;// ����ü
    [SerializeField] private GameObject projectileSpawnPoint;

    StatController meleeStat;
    [SerializeField] private float _checkRange;  // Ÿ�� Ž�� ����
    [SerializeField] private float _attackRange; // ���� ��Ÿ�
    [SerializeField] private float _attackDelay; // ���� �ֱ�
    private float delay; // ���� ������ ���� ����
    private float knockPower; // �˹� ��ġ
    private Vector2 knockBack;
    private float knockBackDuration; // �˹� ���ӽð�
    private bool isDamage; // �ǰ�
    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        //meleeStat = new StatController(_hp, _maxHp, _atk, _def, _moveSpeed, _gold, _crit_Chance, _crit_Multiply, _is_invinsible, _in_invinsible_duration);

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
            if(direction.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if(direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            rigid.velocity = direction * _moveSpeed * Time.deltaTime;
            anim.SetBool("IsRun", true);
        }
    }

   /* public void CheckPlayer()
    {
        target = GameObject.FindWithTag("Player");
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _checkRange)
        {
            Move();
        }
        else return;
    } move�� ����*/  

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile")) //�±� ����
        {
            KnockBack(collision.gameObject); // 
            StartCoroutine("DamageCheck");
            Damaged();
            // ü�� 0���� �Ͻ� Death() �Լ� ȣ��
        }

    }

    IEnumerator DamageCheck()
    {
        isDamage = true;
        yield return new WaitForSeconds(0.5f);
        isDamage = false;
    }
    private void CreateProjectile() 
    {
        //ź�� ���� �� ����
        GameObject bullet = Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity);
        bullet.AddComponent<Rigidbody2D>();
        Vector2 direction = (target.transform.position - transform.position).normalized;
    }
    public void Damaged()
    {
        //meleeStat.Damaged();
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void KnockBack(GameObject collision)
    {
        transform.position = new Vector2(transform.position.x - knockBack.x, transform.position.y - knockBack.y);
        knockBack = (collision.transform.position - transform.position).normalized * knockPower;
    }
}
