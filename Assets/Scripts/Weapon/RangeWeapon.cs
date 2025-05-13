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
    public Color Color => data.projectileData.Color; //ȭ�� ��

    //public GameObject InpactEffect => ProjectileData.impactEffect;

    public int MultiShotCount => data.multiShotCount;        // �� ���� ��� ȭ�� ��
    public float MultiShotAngle => data.multiShotAngle;        // ȭ�� ���� ����

    private float lastAttackTime;
    public StatController Owner;
    public float totalatk_OwnerAndWeapon;
    private Vector3 _originalScale;

    protected override void Start()
    {
        base.Start();
        //projectileManager = ProjectileManager.Instance;
    
        lastAttackTime = -Mathf.Infinity; //ù ������ ��� �����ϵ���
        Owner = GetComponentInParent<StatController>();

        totalatk_OwnerAndWeapon = Atk + Owner.Atk;
    }
   
   
    public override void Attack(Vector3 targetPosition) //��ġ�� �Ķ���ͷ� �޾ƿͼ� ����
    {
        float cooldown = 1f / Speed;
        if (Time.time < lastAttackTime + cooldown)
            return;
        lastAttackTime = Time.time;
        
        // Ȱ ȸ��: Ÿ���� ��Ȯ�� �ٶ󺸵��� transform ȸ��
        Vector2 toTarget = (Vector2)targetPosition - (Vector2)transform.position;
        float bowAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, bowAngle);
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

            // ���� �߻� ���� ���
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
