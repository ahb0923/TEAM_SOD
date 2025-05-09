using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeWeapon : BaseWeapon
{

    [Header("근접 공격 범위 오프셋")]
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
        //  타겟 방향 벡터
        Vector2 dir = (Target.position - transform.position).normalized;
        // 원본 회전 각도(화살 방향이 0°를 바라본다고 가정)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 좌우 뒤집기 기준: 90° 초과 또는 -90° 미만
        bool shouldFlip = angle > 90f || angle < -90f;

        // 실제 적용될 회전 각도
        float appliedAngle = shouldFlip ? angle + 180f : angle;
        transform.localEulerAngles = new Vector3(0f, 0f, appliedAngle);

        // 스케일 X 반전
        Vector3 scale = _originalScale;
        scale.x = shouldFlip
            ? -Mathf.Abs(_originalScale.x)
            : Mathf.Abs(_originalScale.x);
        transform.localScale = scale;
    }
    private void CheckAndAttack()
    {
        if (Target == null)
            return;

        // 1) 거리 계산
        float dist = Vector2.Distance(transform.position, Target.position);

        // 2) 사거리 이내라면 공격
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
        Debug.Log("근접 공격!");

        // 공격 방향에 따라 히트박스 회전
        Vector2 dir = ((Vector2)v - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector2 center = (Vector2)transform.position + hitboxOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // BoxCast로 충돌 검사, 또는 OverlapBox 사용
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

        // (선택) 데미지 이펙트나 사운드 재생 가능
    }

    private float AtkPower => data.attackPower;
    private float AtkSpeed => data.attackSpeed;
    private LayerMask targetLayerMask => target;
}
