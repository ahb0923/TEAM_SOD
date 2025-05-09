using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBow : BaseWeapon
{
    [SerializeField] private WeaponData r_data;  // Range 전용 SO

    public ProjectileData projectileData => r_data.projectileData;
    public float duration => r_data.Duration;

    public int continuousShotCount => r_data.continuousShotCount;   // 연사 수 (1이면 단발)
    public int multiShotCount => r_data.multiShotCount;        // 한 번에 쏘는 화살 수
    public float multiShotAngle => r_data.multiShotAngle;        // 화살 퍼짐 각도
    //private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        //projectileManager = ProjectileManager.Instance;
    }

    public override void Attack()
    {
        base.Attack();

        float angleStep = multiShotAngle;
        int count = multiShotCount;
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
