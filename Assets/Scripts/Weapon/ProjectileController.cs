using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//발사체 충돌과 파괴 처리
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private ProjectileData data;
    [SerializeField] private Transform pivot;

    private Vector2 direction;
    private float elapsedTime;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    
    public float totalAtk;
    public float Critical_Chance;
    public float Critical_Mutiply;
    
    /// <summary>
    /// 발사체를 초기화합니다.
    /// </summary>
    /// <param name="data">ScriptableObject로 정의된 발사체 데이터</param>
    /// <param name="direction">발사 방향 (정규화된 벡터)</param>
    public void Initialize(ProjectileData data, Vector2 direction, float totalatk, float crit_C, float crti_m)
    {
        this.data = data;
        this.direction = direction.normalized;
        this.elapsedTime = 0f;
        
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        
        totalAtk = totalatk;
        Critical_Chance = crit_C;  
        Critical_Mutiply = crti_m;
        Debug.Log(totalAtk);
        Debug.Log(totalAtk + data.attackPower);
        ApplyVisualSettings();
    }
    private void ApplyVisualSettings()
    {
        if (sr != null)
        {
            sr.color = data.Color;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
   
        if (pivot != null)
        {
            pivot.localRotation = direction.x < 0
                ? Quaternion.Euler(180f, 0f, 0f)
                : Quaternion.identity;
        }
    }

    public float GetAttackPower()
    {
        //final_Attack += 무기공격력 + 부모의 공격력
        return totalAtk + data.attackPower;
    }
    public float GetCriChance()
    {
        return Critical_Chance;
    }
    public float GetCriMutiply()
    {
        return Critical_Mutiply;
    }

    private void Update()
    {
        if (data == null) return;
        string key = this.data.name;
        elapsedTime += Time.deltaTime;
        // 수명 경과 시 파괴, 수명은 화살 데이터에서
        if (elapsedTime >= data.lifetime)
        {
            PoolManager.Instance.ReturnObject(key,gameObject);
            return;
        }

        // 이동
        rb.velocity = direction * data.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;
        string key = this.data.name;
        // 레벨(벽 등) 충돌 체크
        if (((1 << layer) & data.layer.value) != 0)
        {
            PoolManager.Instance.ReturnObject(key, gameObject);
            return;
        }

        //// 대상(플레이어/몬스터) 충돌 체크
        //// 플레이어/몬스터 측에서 처리
        //if (((1 << layer) & data.targetLayerMask.value) != 0)
        //{
        //    if (other.TryGetComponent<IDamageable>(out var dmg))
        //        dmg.TakeDamage(data.attackPower);

        //    DestroyProjectile(other.ClosestPoint(transform.position));
        //}
    }

    //임시 코드, 맞았을 경우 이펙트 효과
    public void DestroyProjectile(Vector3 hitPosition)
    {
        // 충돌 이펙트 생성
        if (data.impactEffect != null)
            Instantiate(data.impactEffect, hitPosition, Quaternion.identity);

        // PoolSetting에 등록된 key와 동일하게
        string key = this.data.name;
        PoolManager.Instance.ReturnObject(key, gameObject);
    }
}

