using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Range : Monster
{
    // ������ �ӽ÷� ����
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
        MonsteRotate();
        Attack();
    }

    protected override void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= weapon.AttackRange)
        {
            weapon.Attack(target.transform.position);
            Debug.Log("���Ÿ� ����");
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
                transform.rotation = Quaternion.Euler(0, 0, 0);
                //weaponPivot.transform.rotation = Quaternion.Euler(0, 0, -90); 
            }
            else if (direction.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                //weaponPivot.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            rigid.velocity = direction * _moveSpeed * Time.deltaTime;
            anim.SetBool("IsRun", true);
        }
    }

    protected override void MonsteRotate()
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

}
