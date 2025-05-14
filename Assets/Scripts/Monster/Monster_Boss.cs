using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Boss : Monster
{
    private int attackCount = 0; // 일정 횟수이상 공격 시 그로기
    public RangeWeapon weaponPrefab;
    protected RangeWeapon weapon;
    public SpriteRenderer sprite;
    protected bool isPattern = false;
    protected bool isGroggy = false;

    protected override void Awake()
    {
        base.Awake();
        if (weapon == null)
        {
            weapon = Instantiate(weaponPrefab, weaponPivot.transform);
        }
    }

    protected override void Update()
    {
        if (!isPattern) 
        {
            delay += Time.deltaTime; 
        }
        if (delay > _attackDelay && !isGroggy && !isPattern)
        {
            Pattern();
        }
        if(attackCount == 5)
        {
            StartCoroutine("BossGroggy");
        }
        MonsterRotate();
    }
    protected override void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= weapon.AttackRange)
        {
            weapon.Attack(target.transform.position);
        }
    }
    protected override void MonsterRotate()
    {
        if(transform.position.x - target.transform.position.x > 0) 
        {
            sprite.flipX = true;
        }
        else if (transform.position.x - target.transform.position.x > 0)
        {
            sprite.flipX = false;
        }
    }

    protected void Pattern1() 
    {
        delay = 0;
        isPattern = true;
        weapon.data.multiShotAngle = 20;
        weapon.data.multiShotCount = 10;
        Attack();
        isPattern = false;
    }

    protected void Pattern2()
    {
        delay = 0;
        isPattern = true;
        float firstSpeed = weapon.data.attackSpeed;
        //float firstCoolDown =
        weapon.data.attackSpeed = 5f;
        weapon.data.multiShotAngle = 1;
        weapon.data.multiShotCount = 1;
        for (int i = 0; i < 3; i++)
        {
            Attack();
        }
        weapon.data.attackSpeed = firstSpeed;
        isPattern = false;
    }

    protected void Pattern3()
    {
        delay = 0;
        isPattern = true;
        weapon.data.multiShotAngle = 5;
        weapon.data.multiShotCount = 5;
        Attack();
        isPattern = false;
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
                Debug.Log("패턴 1");              
                Pattern1();
                attackCount++;             
                break;
            case 2:
                Debug.Log("패턴 2");              
                Pattern2();               
                attackCount++;              
                break;
            case 3:
                Debug.Log("패턴 3");              
                Pattern3();
                attackCount++;               
                break;
        }
    }

    protected IEnumerator BossGroggy()
    {
        Debug.Log("그로기 상태입니다.");
        attackCount = 0;
        isGroggy = true;
        yield return new WaitForSeconds(5.0f);
        isGroggy = false;
    }
}
