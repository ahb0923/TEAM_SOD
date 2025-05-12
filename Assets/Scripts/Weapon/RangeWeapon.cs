using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : BaseWeapon
{
    [SerializeField] private WeaponData r_data;  // Range 전용 SO
    [SerializeField] private Transform projectileSpawnPoint;

    public ProjectileData projectileData;
    public float duration => r_data.projectileData.lifetime;

    //public int continuousShotCount => r_data.continuousShotCount;   // 연사 수 (1이면 단발)
    public int multiShotCount => r_data.multiShotCount;        // 한 번에 쏘는 화살 수
    public float multiShotAngle => r_data.multiShotAngle;        // 화살 퍼짐 각도

    public Color color => r_data.projectileData.Color; //화살 색
    public float projSpeed => r_data.projectileData.moveSpeed;

    private float lastAttackTime;

    public StatController Owner;
    public float totalatk_OwnerAndWeapon;

    //private ProjectileManager projectileManager;
    
    protected override void Start()
    {
        base.Start();
        //projectileManager = ProjectileManager.Instance;

        lastAttackTime = -Mathf.Infinity; //첫 공격이 즉시 가능하도록
        Owner = GetComponentInParent<StatController>();
        totalatk_OwnerAndWeapon = r_data.attackPower + Owner.Atk;
    }

    public override void Attack(Vector3 targetPosition) //위치를 파라미터로 받아와서 공격
    {
        float cooldown = 1f / r_data.attackSpeed;
        if (Time.time < lastAttackTime + cooldown)
            return;
        lastAttackTime = Time.time;

        // 활 회전: 타겟을 정확히 바라보도록 transform 회전
        Vector2 toTarget = (Vector2)targetPosition - (Vector2)transform.position;
        float bowAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, bowAngle);
        base.Attack(targetPosition);

        int count = multiShotCount;
        float angleStep = multiShotAngle;
        float startAngle = -(count - 1) / 2f * angleStep;

        
        Vector2 spawnPos = projectileSpawnPoint.position;
        
        float baseZ = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;

        for (int i = 0; i < count; i++)
        {

            float localAngle = startAngle + angleStep * i;
            float zAngle = baseZ + localAngle;

            // 최종 발사 방향 계산
            Vector2 dir = Quaternion.Euler(0f, 0f, zAngle) * Vector2.right;

            ProjectileManager.Instance.SpawnProjectile(
                projectileData,
                spawnPos,
                dir,
                totalatk_OwnerAndWeapon
            );
        }
    }
}
