using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Monster_Range : Monster
{
    // 스탯은 임시로 적용
    public RangeWeapon weaponPrefab;
    protected RangeWeapon weapon;
    protected override void Awake()
    {
        base.Awake();
        if (weapon == null)
        {
            weapon = Instantiate(weaponPrefab, weaponPivot.transform);
        }
    }
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Move();
        //MonsterRotate();
        Attack();
        Debug.Log("공격사거리" + weapon.AttackRange);
    }

    protected override void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= weapon.AttackRange)
        {
            weapon.Attack(target.transform.position);
            Debug.Log("원거리 공격");
            //delay = 0;
        }
    }
    protected override void Move()
    {
        if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) > _checkRange)
        {
            anim.SetBool("IsRun", false);
            rigid.velocity = Vector2.zero;
            return;
        }
        if (Mathf.Abs(Vector2.Distance(transform.position, target.transform.position)) <= weapon.AttackRange)
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

    protected override void MonsterRotate()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            weapon.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public override void Death()
    {
        PoolManager.Instance.ReturnObject(MONSTER_KEY.Range_Test.ToString(), this.gameObject);
    }
}
