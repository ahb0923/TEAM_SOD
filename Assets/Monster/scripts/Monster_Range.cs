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
    private float _moveSpeed = 20f;
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
        //meleeStat = new StatController(_hp, _maxHp, _atk, _def, _moveSpeed, _gold, _crit_Chance, _crit_Multiply, _is_invinsible, _in_invinsible_duration);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;
        CheckPlayer();
        Attack();
    }

    public void Move(GameObject player)
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, player.transform.position)) <= _attackRange) return;
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rigid.velocity = direction * _moveSpeed * Time.deltaTime;
        isMove = true;
        anim.SetBool("IsRun", true);
    }

    public void CheckPlayer()
    {
        target = GameObject.FindWithTag("Player");
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _checkRange)
        {
            Move(target);
        }
    }

    public void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _attackRange && delay >= _attackDelay)
        {
            //공격
            
            delay = 0;
        }
        if (distance > _attackRange)
        {
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile")) //태그 예시
        {
            Damaged();
        }

    }

    private void CreateProjectile() { }
    public void Damaged()
    {
        //meleeStat.Damaged();
    }

    public void Death()
    {

    }
}
