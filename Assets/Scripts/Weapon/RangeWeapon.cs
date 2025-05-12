using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : BaseWeapon
{
    [SerializeField] private WeaponData r_data;  // Range ���� SO
    [SerializeField] private Transform projectileSpawnPoint;

    public ProjectileData projectileData;
    public float duration => r_data.projectileData.lifetime;

    //public int continuousShotCount => r_data.continuousShotCount;   // ���� �� (1�̸� �ܹ�)
    public int multiShotCount => r_data.multiShotCount;        // �� ���� ��� ȭ�� ��
    public float multiShotAngle => r_data.multiShotAngle;        // ȭ�� ���� ����

    public Color color => r_data.projectileData.Color; //ȭ�� ��
    public float projSpeed => r_data.projectileData.moveSpeed;

    private float lastAttackTime;

    public StatController Owner;
    public float totalatk_OwnerAndWeapon;

    //private ProjectileManager projectileManager;
    
    protected override void Start()
    {
        base.Start();
        //projectileManager = ProjectileManager.Instance;

        lastAttackTime = -Mathf.Infinity; //ù ������ ��� �����ϵ���
        Owner = GetComponentInParent<StatController>();
        totalatk_OwnerAndWeapon = r_data.attackPower + Owner.Atk;
    }

    public override void Attack(Vector3 targetPosition) //��ġ�� �Ķ���ͷ� �޾ƿͼ� ����
    {
        float cooldown = 1f / r_data.attackSpeed;
        if (Time.time < lastAttackTime + cooldown)
            return;
        lastAttackTime = Time.time;

        // Ȱ ȸ��: Ÿ���� ��Ȯ�� �ٶ󺸵��� transform ȸ��
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

            // ���� �߻� ���� ���
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
