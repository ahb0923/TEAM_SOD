using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private ProjectileData data;
    private Vector2 direction;
    private float elapsedTime;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    /// <summary>
    /// 발사체를 초기화합니다.
    /// </summary>
    /// <param name="data">ScriptableObject로 정의된 발사체 데이터</param>
    /// <param name="direction">발사 방향 (정규화된 벡터)</param>
    public void Initialize(ProjectileData data, Vector2 direction)
    {
        this.data = data;
        this.direction = direction.normalized;
        this.elapsedTime = 0f;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        // 발사체 색상 설정
        if (sr != null)
            sr.color = data.Color;

        // 회전을 통해 방향 맞추기
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        if (data == null) return;

        elapsedTime += Time.deltaTime;
        // 수명 경과 시 파괴
        if (elapsedTime >= data.lifetime)
        {
            DestroyProjectile(transform.position);
            return;
        }

        // 이동
        rb.velocity = direction * data.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        //// 레벨(벽 등) 충돌 체크
        //if (((1 << layer) & data.levelCollisionLayer.value) != 0)
        //{
        //    DestroyProjectile(other.ClosestPoint(transform.position));
        //    return;
        //}

        //// 대상(플레이어/몬스터) 충돌 체크
        //if (((1 << layer) & data.targetLayerMask.value) != 0)
        //{
        //    if (other.TryGetComponent<IDamageable>(out var dmg))
        //        dmg.TakeDamage(data.attackPower);

        //    DestroyProjectile(other.ClosestPoint(transform.position));
        //}
    }

    private void DestroyProjectile(Vector3 hitPosition)
    {
        // 충돌 이펙트 생성
        if (data.impactEffect != null)
            Instantiate(data.impactEffect, hitPosition, Quaternion.identity);

        Destroy(gameObject);
    }
}
