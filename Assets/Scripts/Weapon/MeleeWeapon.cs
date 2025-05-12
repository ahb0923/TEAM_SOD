using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{

    [Header("근접 공격 범위 오프셋")]
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

        // 공격 방향에 따라 히트박스 회전
        Vector2 dir = ((Vector2)v - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector2 center = (Vector2)transform.position + hitboxOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        //// BoxCast로 충돌 검사, 또는 OverlapBox 사용
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

        // (선택) 데미지 이펙트나 사운드 재생 가능
    }
}
