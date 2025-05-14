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

    public int MultiShotCount => data.multiShotCount ;        // �� ���� ��� ȭ�� ��
    public float MultiShotAngle => data.multiShotAngle;        // ȭ�� ���� ����

    private float lastAttackTime;
    public StatController owner;
    public float totalatk_OwnerAndWeapon => data.attackPower + owner.Atk;
    public float crit_c => owner.Crit_Chance;
    public float crit_m => owner.Crit_Multiply;

    private Vector3 originalScale;

    protected override void Awake()
    {     
        owner = GetComponentInParent<StatController>();
    }

    protected override void Start()
    {
        lastAttackTime = -Mathf.Infinity; //ù ������ ��� �����ϵ���
        originalScale = transform.localScale;
    }
   
   
    public override void Attack(Vector3 targetPosition) //��ġ�� �Ķ���ͷ� �޾ƿͼ� ����
    {
        if (!AttackCoolTime())
            return;
        Debug.Log(totalatk_OwnerAndWeapon);
        base.Attack(targetPosition);

        //Ÿ���� ���ؼ� ȸ��
        FaceTarget(targetPosition);

        //����ü ���� ��û -> projectileManager
        SpawnProjectiles(targetPosition);
    }
    private bool AttackCoolTime()
    {
        float cooldown = 1f / Speed;
        if (Time.time < lastAttackTime + cooldown)
            return false;

        lastAttackTime = Time.time;
        return true;
    }

    private void FaceTarget(Vector3 targetPosition)
    {
        Vector2 toTarget = ((Vector2)targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
        bool shouldFlip = angle > 90f || angle < -90f;

        
        float appliedAngle = shouldFlip ? angle + 180f : angle;
        transform.localEulerAngles = new Vector3(0f, 0f, appliedAngle);

       
        Vector3 scale = originalScale;
        scale.x = shouldFlip ? -Mathf.Abs(originalScale.x) : Mathf.Abs(originalScale.x);
        transform.localScale = scale;
    }

    private void SpawnProjectiles(Vector3 targetPosition)
    {
        Vector2 baseDirection = ((Vector2)targetPosition - (Vector2)transform.position).normalized;
        Vector2 spawnPos = projectileSpawnPoint.position;
        float baseAngle = Mathf.Atan2(baseDirection.y, baseDirection.x) * Mathf.Rad2Deg;

        for (int i = 0; i < MultiShotCount; i++)
        {
            float offset = -(MultiShotCount - 1) * 0.5f * MultiShotAngle + i * MultiShotAngle;
            float shotAngle = baseAngle + offset;
            Vector2 dir = Quaternion.Euler(0f, 0f, shotAngle) * Vector2.right;

            ProjectileManager.Instance.SpawnProjectile(
                ProjectileData,
                spawnPos,
                dir,
                totalatk_OwnerAndWeapon,
                crit_c,
                crit_m
            );
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (data == null) return;

        // ���� ��ġ �������� attackRange ���� ǥ��
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);  // ��Ȳ ������
        Gizmos.DrawWireSphere(transform.position, data.attackRange);
    }
#endif
}
