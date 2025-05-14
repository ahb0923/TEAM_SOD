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

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
        Owner = GetComponentInParent<StatController>();
        Owner_Moster = this.transform.parent.parent.gameObject;
    }
    protected override void Start()
    {
        base.Start();
        lastAttackTime = -Mathf.Infinity;
        _originalScale = transform.localScale;

        Target = Owner_Moster.GetComponent<Monster_Melee>().target.transform;      
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

    public float GetCriChance()
    {
        return Owner.Crit_Chance;
    }
    public float GetCriMutiply()
    {
        return Owner.Crit_Multiply;
    }


    public override void Attack(Vector3 v)
    {
        if (!AttackCoolTime())
            return;

        base.Attack(v);
        Debug.Log("근접공격");

        // 공격 방향에 따라 히트박스 회전
        Vector2 dir = ((Vector2)v - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Vector2 center = (Vector2)transform.position + hitboxOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

    }
    private bool AttackCoolTime()
    {
        float cooldown = 1f / Speed;
        if (Time.time < lastAttackTime + cooldown)
            return false;

        lastAttackTime = Time.time;
        return true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (data == null) return;

        // 회전 및 스케일 적용된 상태에서 Offset, Size에 따라 박스 표시
        Vector3 center = transform.position + (Vector3)hitboxOffset;
        Quaternion rot = transform.rotation;
        Vector3 size = new Vector3(hitboxSize.x, hitboxSize.y, 1f);

        Gizmos.color = new Color(0f, 1f, 0f, 0.5f);   // 초록 반투명
        Matrix4x4 old = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(center, rot, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, size);
        Gizmos.matrix = old;
    }
#endif

}
