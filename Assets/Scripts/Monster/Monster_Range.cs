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
        Attack();
    }

    protected override void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _attackRange)
        {
            weapon.Attack(target.transform.position);
            Debug.Log("���Ÿ� ����");
            //delay = 0;
        }
        else
        {
            weapon.animator.SetBool("IsAttack", false);
        }

    }
    private void CreateProjectile()
    {
        //ź�� ���� �� ����     
        Vector2 direction = (target.transform.position - transform.position).normalized;
    }
}
