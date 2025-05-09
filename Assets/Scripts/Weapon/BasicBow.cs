using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBow : BaseWeapon
{
    [SerializeField] private WeaponData r_data;  // Range ���� SO

    public ProjectileData projectileData => r_data.projectileData;
    public float duration => r_data.Duration;

    public int continuousShotCount => r_data.continuousShotCount;   // ���� �� (1�̸� �ܹ�)
    public int multiShotCount => r_data.multiShotCount;        // �� ���� ��� ȭ�� ��
    public float multiShotAngle => r_data.multiShotAngle;        // ȭ�� ���� ����
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
            //    data  // data �ȿ� Power, Speed �� ��� ����
            //);
        }
    }
}
