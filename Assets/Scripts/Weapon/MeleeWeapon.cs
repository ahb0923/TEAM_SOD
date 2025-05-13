using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeWeapon : BaseWeapon
{

    [Header("근접 공격 범위 오프셋")]
    [SerializeField] private Vector2 hitboxSize = Vector2.one;
    [SerializeField] private Vector2 hitboxOffset = Vector2.zero;

    private Vector3 _originalScale;
    public Transform Target;
    public StatController Owner;

    public GameObject Owner_Moster;

    private float lastAttackTime;
    protected override void Start()
    {
        base.Start();
        lastAttackTime = -Mathf.Infinity;
        animator = GetComponentInChildren<Animator>();
        _originalScale = transform.localScale;

        Owner = GetComponentInParent<StatController>();

        //Owner_Moster = GetComponentInParent<GameObject>();
        Target = Owner.GetComponent<Monster_Melee>().target.transform;

        // ② AnimatorController 할당 확인
        if (animator == null)
            Debug.LogError("Animator를 찾을 수 없습니다.");
        else if (animator.runtimeAnimatorController == null)
            Debug.LogError("AnimatorController가 할당되지 않았습니다.");

       
    }

    void Update()
    {
        FlipTowardsTarget();
        CheckAndAttack();
    }
    private void FlipTowardsTarget()
    {
        Vector2 dir = (Target.position - transform.position).normalized;
    
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;


        bool shouldFlip = angle > 90f || angle < -90f;

    
        float appliedAngle = shouldFlip ? angle + 180f : angle;
        transform.localEulerAngles = new Vector3(0f, 0f, appliedAngle);

      
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

      
        float dist = Vector2.Distance(transform.position, Target.position);

       
        if (dist <= data.attackRange)
        {
            Attack(Target.position);
        }
        
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
        base.Attack(v);
        Debug.Log("근접공격");
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
            data.layer
        );

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
