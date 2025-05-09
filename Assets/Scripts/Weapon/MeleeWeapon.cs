using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{

    [Header("���� ���� ���� ������")]
    [SerializeField] private Vector2 hitboxSize = Vector2.one;
    [SerializeField] private Vector2 hitboxOffset = Vector2.zero;


    public Transform Target;
    private float lastAttackTime;
    private Vector3 _originalScale;



    protected override void Start()
    {
        base.Start();
        _originalScale = transform.localScale;
        lastAttackTime = -Mathf.Infinity;
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("IsAttack", false);
    }
    void Update()
    {
        FlipTowardsTarget();
        CheckAndAttack();
    }
    private void FlipTowardsTarget()
    {
        if (Target == null) return;
        float dirX = Target.position.x - transform.position.x;
        Vector3 scale = _originalScale;
        scale.x *= Mathf.Sign(dirX);
        transform.localScale = scale;
    }
    private void CheckAndAttack()
    {
        if (Target == null)
            return;

        // 1) �Ÿ� ���
        float dist = Vector2.Distance(transform.position, Target.position);

        // 2) ��Ÿ� �̳���� ����
        if (dist <= data.attackRange)
        {
            Attack(Target.position);
        }
        else { animator.SetBool("IsAttack", false); }
    }

    public override void Attack(Vector3 v)
    {
        float cooldown = 1f / AtkSpeed;
        if (Time.time < lastAttackTime + cooldown)
            return;

        lastAttackTime = Time.time;
        AttackAnimation();
        Debug.Log("���� ����!");

        // ���� ���⿡ ���� ��Ʈ�ڽ� ȸ��
        Vector2 dir = ((Vector2)v - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector2 center = (Vector2)transform.position + hitboxOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // BoxCast�� �浹 �˻�, �Ǵ� OverlapBox ���
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            center,
            hitboxSize * WeaponSize,
            angle,
            Vector2.zero,
            0f,
            targetLayerMask
        );

        foreach (var hit in hits)
        {
            //if (hit.collider != null && hit.collider.TryGetComponent<IDamageable>(out var dmg))
            //{
            //    dmg.TakeDamage(AtkPower);
            //}
        }

        // (����) ������ ����Ʈ�� ���� ��� ����
    }

    private float AtkPower => data.attackPower;
    private float AtkSpeed => data.attackSpeed;
    private LayerMask targetLayerMask => target;
}
