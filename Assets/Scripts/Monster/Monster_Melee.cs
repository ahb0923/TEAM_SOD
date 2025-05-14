using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Monster_Melee : Monster
{
    public MeleeWeapon weaponPrefab;
    protected BaseWeapon weapon;
    private MONSTER_KEY key;
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
        base.Update();        
        //Move();
        //MonsterRotate();
    }
    protected override void Attack()
    {
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= _attackRange && delay >= _attackDelay)
        {
        }
    }

    public override void Death()
    {
        PoolManager.Instance.ReturnObject(MONSTER_KEY.Melee_Test.ToString(), this.gameObject);
    }
}
