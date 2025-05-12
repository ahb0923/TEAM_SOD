using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{

    [Header("���� ���� ���� ������")]
    [SerializeField] private Vector2 hitboxSize = Vector2.one;
    [SerializeField] private Vector2 hitboxOffset = Vector2.zero;

   public StatController Owner;
    
    private float lastAttackTime;
    protected override void Start()
    {
        base.Start();
        lastAttackTime = -Mathf.Infinity;
        animator = GetComponentInChildren<Animator>();

        Owner = GetComponentInParent<StatController>();
    }

    public float GetAttackPower()
    {
        float Total = data.attackPower + Owner.Atk;
        
        return Total;
    }

    public override void Attack(Vector3 v)
    {
        float cooldown = 1f / Speed;
        if (Time.time < lastAttackTime + cooldown)
            return;

        lastAttackTime = Time.time;
        AttackAnimation();
        animator.SetTrigger("IsAttack");

        // ���� ���⿡ ���� ��Ʈ�ڽ� ȸ��
        Vector2 dir = ((Vector2)v - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector2 center = (Vector2)transform.position + hitboxOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        //// BoxCast�� �浹 �˻�, �Ǵ� OverlapBox ���
        //RaycastHit2D[] hits = Physics2D.BoxCastAll(
        //    center,
        //    hitboxSize * WeaponSize,
        //    angle,
        //    Vector2.zero,
        //    0f,
        //    targetLayerMask
        //);

        //foreach (var hit in hits)
        //{
        //    //if (hit.collider != null && hit.collider.TryGetComponent<IDamageable>(out var dmg))
        //    //{
        //    //    dmg.TakeDamage(AtkPower);
        //    //}
        //}

        // (����) ������ ����Ʈ�� ���� ��� ����
    }
}
