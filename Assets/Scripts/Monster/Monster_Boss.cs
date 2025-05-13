using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Boss : Monster
{
    [SerializeField] GameObject[] attackPattern;
    private bool isAttack;
    private int attackCount; // 일정 횟수이상 공격 시 그로기
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
                SpriteRenderer[] zoneImg = attackPattern[ran].GetComponentsInChildren<SpriteRenderer>();
                BoxCollider2D[] collider = attackPattern[ran].GetComponentsInChildren<BoxCollider2D>();
                for (int i = 0; i < zoneImg.Length; i++)
                {
                    //zoneImg[i].color.a = 0.0f;
                } 

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
}
