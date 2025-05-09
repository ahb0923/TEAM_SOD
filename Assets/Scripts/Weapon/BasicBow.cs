using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBow : BaseWeapon
{
    [SerializeField] private WeaponData r_data;  // Range ���� SO
    [SerializeField] private Transform projectileSpawnPoint;

    public ProjectileData projectileData => r_data.projectileData;
    public float duration => r_data.projectileData.lifetime;

    //public int continuousShotCount => r_data.continuousShotCount;   // ���� �� (1�̸� �ܹ�)
    public int multiShotCount => r_data.multiShotCount;        // �� ���� ��� ȭ�� ��
    public float multiShotAngle => r_data.multiShotAngle;        // ȭ�� ���� ����

    public Color color => r_data.projectileData.Color; //ȭ�� ��
    public float projSpeed => r_data.projectileData.moveSpeed;

    private float lastAttackTime;

    //private ProjectileManager projectileManager;
    
    protected override void Start()
    {
        base.Start();
        //projectileManager = ProjectileManager.Instance;

        lastAttackTime = -Mathf.Infinity; //ù ������ ��� �����ϵ���
    }

    public override void AttackTest() //���Ŀ� ��ġ ���� �޾Ƽ� �ش� �������� �߻��ϵ���
    {
        float cooldown = 1f / r_data.attackSpeed;
        if (Time.time < lastAttackTime + cooldown)
            return;
        lastAttackTime = Time.time;
        base.AttackTest();

        int count = r_data.multiShotCount;
        float angleStep = r_data.multiShotAngle;
        float startAngle = -(count - 1) / 2f * angleStep;

        // �⺻ ������ SpawnPoint�� ���ϴ� ����(�ӽ�)
        float baseZ = projectileSpawnPoint.eulerAngles.z;
        Vector2 spawnPos = projectileSpawnPoint.position;

        for (int i = 0; i < count; i++)
        {
    
            float localAngle = startAngle + angleStep * i;
            float zAngle = baseZ + localAngle;

            // ���� �߻� ���� ���
            Vector2 dir = Quaternion.Euler(0f, 0f, zAngle) * Vector2.right;

            ProjectileManager.Instance.SpawnProjectile(
                r_data.projectileData,
                spawnPos,
                dir
            );
        }
    }

    public override void Attack(Vector3 targetPosition) //���Ŀ� ��ġ ���� �޾Ƽ� �ش� �������� �߻��ϵ���
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

            // ���� �߻� ���� ���
            Vector2 dir = Quaternion.Euler(0f, 0f, zAngle) * Vector2.right;

            ProjectileManager.Instance.SpawnProjectile(
                r_data.projectileData,
                spawnPos,
                dir
            );
        }
    }
}
