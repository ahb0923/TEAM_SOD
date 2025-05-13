using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangeWeapon : BaseWeapon
{
   
    [SerializeField] private Transform projectileSpawnPoint;

    public ProjectileData ProjectileData => data.projectileData;
    public float ProjSpeed => ProjectileData.moveSpeed;
    public float Duration => ProjectileData.lifetime; 
    public Color Color => data.projectileData.Color; //화살 색

    //public GameObject InpactEffect => ProjectileData.impactEffect;

    public int MultiShotCount => data.multiShotCount;        // 한 번에 쏘는 화살 수
    public float MultiShotAngle => data.multiShotAngle;        // 화살 퍼짐 각도

    private float lastAttackTime;
    public StatController Owner;
    public float totalatk_OwnerAndWeapon;
    private Vector3 _originalScale;

    protected override void Start()
    {
        base.Start();
        //projectileManager = ProjectileManager.Instance;
    
        lastAttackTime = -Mathf.Infinity; //첫 공격이 즉시 가능하도록
        Owner = GetComponentInParent<StatController>();

        totalatk_OwnerAndWeapon = Atk + Owner.Atk;
        _originalScale = transform.localScale;
    }
   
   
    public override void Attack(Vector3 targetPosition) //위치를 파라미터로 받아와서 공격
    {
        float cooldown = 1f / Speed;
        if (Time.time < lastAttackTime + cooldown)
            return;
        lastAttackTime = Time.time;

        // 활 회전: 타겟을 정확히 바라보도록 transform 회전
        Vector2 toTarget = (Vector2)targetPosition - (Vector2)transform.position;
        float baseAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;

        bool shouldFlip = baseAngle > 90f || baseAngle < -90f;

        float appliedAngle = shouldFlip ? baseAngle + 180f : baseAngle;
        transform.localEulerAngles = new Vector3(0f, 0f, appliedAngle);

        Vector3 scale = _originalScale;
        scale.x = shouldFlip
            ? -Mathf.Abs(_originalScale.x)
            : Mathf.Abs(_originalScale.x);
        transform.localScale = scale;


        base.Attack(targetPosition);

        int count = MultiShotCount;
        float angleStep = MultiShotAngle;
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
                ProjectileData,
                spawnPos,
                dir,
                totalatk_OwnerAndWeapon
            );
        }
    }
}
