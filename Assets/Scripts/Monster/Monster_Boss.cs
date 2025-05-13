using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Boss : Monster
{
    private int attackCount; // 일정 횟수이상 공격 시 그로기
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
    protected override void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _attackRange && delay >= _attackDelay)
        {
            Pattern();
        }
    }

    protected override void Move()
    {
        return;
    }

    private void Pattern()
    {
        int ran = Random.Range(1, 4);
        switch (ran)
        {
            case 1:         
                attackCount++;
                break;
            case 2:
                attackCount++;
                break;
            case 3:
                attackCount++;
                break;
        }
    }

    private void Pattern1()
    {
        //weapon.
    }
}
