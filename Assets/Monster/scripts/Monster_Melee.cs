using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.GraphicsBuffer;

public class Monster_Melee : MonoBehaviour
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

    private bool isMove;
    [SerializeField] private GameObject meleeAttackZone;

    StatController meleeStat;
    [SerializeField] private float _checkRange;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _attackDelay; // 공격 주기

    public PlayerData data;
    private float delay; // 공격 딜레이
    private float lastAttack;
    private float knockPower; // 넉백 수치
    private Vector2 knockBack;
    private bool isDamage; // 피격

    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        target = GameObject.FindWithTag("Player");
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
        if (distance <= _attackRange && delay >=_attackDelay)
        {
            //공격
            meleeAttackZone.gameObject.SetActive(true);
            delay = 0;
        }
        if(distance > _attackRange)
        {
            meleeAttackZone.gameObject.SetActive(false);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile") || !isDamage ) //태그 예시
        {
            KnockBack(collision.gameObject);
            DamageCheck(); // 데미지 체크 및 데미지 계산
        }

    }

    IEnumerator DamageCheck()
    {
        isDamage = true;
        Damaged();
        yield return new WaitForSeconds(0.5f);
        isDamage = false;
    }
    public void Damaged()
    {
        //meleeStat.Damaged();
    }

    public void Death()
    {
        // statController 에서 호출
    }

    public void KnockBack(GameObject collision)
    {
        transform.position = new Vector2(transform.position.x + knockBack.x, transform.position.y + knockBack.y);
        knockBack = (collision.transform.position - transform.position).normalized * knockPower;
    }
}
