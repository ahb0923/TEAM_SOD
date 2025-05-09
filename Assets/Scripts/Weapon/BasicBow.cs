using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBow : BaseWeapon
{
    [SerializeField] private RangeWeaponData r_data;  // Range 전용 SO

    public bool IsOnKnockback => r_data.isOnKnockback;
    public float KnockbackPower => r_data.knockbackPower;
    public float KnockbackTime => r_data.knockbackTime;
    //private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        //projectileManager = ProjectileManager.Instance;
    }

    public override void Attack()
    {
        base.Attack();

        float angleStep = r_data.multipleProjectilesAngle;
        int count = r_data.numberOfProjectilesPerShot;
        float startAngle = -(count / 2f) * angleStep + 0.5f * angleStep;

        for (int i = 0; i < count; i++)
        {
            //float angle = startAngle + angleStep * i + Random.Range(-data.spread, data.spread);
            //Vector2 dir = RotateVector(Controller.LookDirection, angle);
            //projectileManager.ShootBullet(
            //    data.projectilePrefab,
            //    projectileSpawnPosition.position,
            //    dir,
            //    data  // data 안에 Power, Speed 등 모두 포함
            //);
        }
    }
}
