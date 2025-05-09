using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Range : MonoBehaviour
{
    // 스탯은 임시로 적용
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

    public GameObject target;

    public GameObject projectile;
    private bool isMove;
    [SerializeField] private GameObject projectileSpawnPoint;

    StatController meleeStat;
    [SerializeField] private float _checkRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay; // 공격 주기
    private float delay; // 공격 딜레이
    private float lastAttack;

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
    }*/

    public void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _attackRange && delay >= _attackDelay)
        {
            //공격
            Debug.Log("원거리 공격");
            delay = 0;
        }
        else
        {
            delay += Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile")) //태그 예시
        {
            Damaged();
        }

    }

    private void CreateProjectile() 
    {
        //탄쪽 머지 후 수정
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

    }
}
