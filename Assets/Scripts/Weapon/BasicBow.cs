using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBow : BaseWeapon
{
    [SerializeField] private WeaponData r_data;  // Range 전용 SO
    [SerializeField] private Transform projectileSpawnPoint;

    public ProjectileData projectileData => r_data.projectileData;
    public float duration => r_data.projectileData.lifetime;

    //public int continuousShotCount => r_data.continuousShotCount;   // 연사 수 (1이면 단발)
    public int multiShotCount => r_data.multiShotCount;        // 한 번에 쏘는 화살 수
    public float multiShotAngle => r_data.multiShotAngle;        // 화살 퍼짐 각도

    public Color color => r_data.projectileData.Color; //화살 색
    public float projSpeed => r_data.projectileData.moveSpeed;

    private float lastAttackTime;

    //private ProjectileManager projectileManager;
    
    protected override void Start()
    {
        base.Start();
        //projectileManager = ProjectileManager.Instance;

        lastAttackTime = -Mathf.Infinity; //첫 공격이 즉시 가능하도록
    }

    public override void AttackTest() //이후에 위치 값을 받아서 해당 방향으로 발사하도록
    {
        float cooldown = 1f / r_data.attackSpeed;
        if (Time.time < lastAttackTime + cooldown)
            return;
        lastAttackTime = Time.time;
        base.AttackTest();

        int count = r_data.multiShotCount;
        float angleStep = r_data.multiShotAngle;
        float startAngle = -(count - 1) / 2f * angleStep;

        // 기본 방향은 SpawnPoint의 향하는 방향(임시)
        float baseZ = projectileSpawnPoint.eulerAngles.z;
        Vector2 spawnPos = projectileSpawnPoint.position;

        for (int i = 0; i < count; i++)
        {
    
            float localAngle = startAngle + angleStep * i;
            float zAngle = baseZ + localAngle;

            // 최종 발사 방향 계산
            Vector2 dir = Quaternion.Euler(0f, 0f, zAngle) * Vector2.right;

            ProjectileManager.Instance.SpawnProjectile(
                r_data.projectileData,
                spawnPos,
                dir
            );
        }
    }

    public override void Attack(Vector3 targetPosition) //이후에 위치 값을 받아서 해당 방향으로 발사하도록
    {
        float cooldown = 1f / r_data.attackSpeed;
        if (Time.time < lastAttackTime + cooldown)
            return;
        lastAttackTime = Time.time;
        base.Attack(targetPosition);

        int count = r_data.multiShotCount;
        float angleStep = r_data.multiShotAngle;
        float startAngle = -(count - 1) / 2f * angleStep;

        
        Vector2 spawnPos = projectileSpawnPoint.position;
        Vector2 toTarget = (Vector2)targetPosition - spawnPos;
        float baseZ = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;

        for (int i = 0; i < count; i++)
        {

            float localAngle = startAngle + angleStep * i;
            float zAngle = baseZ + localAngle;

            // 최종 발사 방향 계산
            Vector2 dir = Quaternion.Euler(0f, 0f, zAngle) * Vector2.right;

            ProjectileManager.Instance.SpawnProjectile(
                r_data.projectileData,
                spawnPos,
                dir
            );
        }
    }
}
