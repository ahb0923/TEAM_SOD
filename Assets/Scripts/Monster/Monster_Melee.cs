using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Monster_Melee : Monster
{
    protected override void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _attackRange && delay >= _attackDelay)
        {
            weapon.Attack(target.transform.position);
            delay = 0;
        }
    }
}
