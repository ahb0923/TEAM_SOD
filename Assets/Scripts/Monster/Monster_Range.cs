using System.Collections;
using System.Collections.Generic;
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
        Attack();
    }

    protected override void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _attackRange)
        {
            weapon.Attack(target.transform.position);
            Debug.Log("원거리 공격");
            //delay = 0;
        }
        else
        {
            weapon.animator.SetBool("IsAttack", false);
        }

    }
    private void CreateProjectile()
    {
        //탄쪽 머지 후 수정     
        Vector2 direction = (target.transform.position - transform.position).normalized;
    }
}
