using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�߻�ü �浹�� �ı� ó��
public class ProjectileController : MonoBehaviour
{
    private ProjectileData data;
    private Vector2 direction;
    private float elapsedTime;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    /// <summary>
    /// �߻�ü�� �ʱ�ȭ�մϴ�.
    /// </summary>
    /// <param name="data">ScriptableObject�� ���ǵ� �߻�ü ������</param>
    /// <param name="direction">�߻� ���� (����ȭ�� ����)</param>
    public void Initialize(ProjectileData data, Vector2 direction)
    {
        this.data = data;
        this.direction = direction.normalized;
        this.elapsedTime = 0f;

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        // �߻�ü ���� ����
        if (sr != null)
            sr.color = data.Color;

        // ȸ���� ���� ���� ���߱�
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Update()
    {
        if (data == null) return;

        elapsedTime += Time.deltaTime;
        // ���� ��� �� �ı�, ������ ȭ�� �����Ϳ���
        if (elapsedTime >= data.lifetime)
        {
            DestroyProjectile(transform.position);
            return;
        }

        // �̵�
        rb.velocity = direction * data.moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        //// ����(�� ��) �浹 üũ
        //if (((1 << layer) & data.levelCollisionLayer.value) != 0)
        //{
        //    DestroyProjectile(other.ClosestPoint(transform.position));
        //    return;
        //}

        //// ���(�÷��̾�/����) �浹 üũ
        //if (((1 << layer) & data.targetLayerMask.value) != 0)
        //{
        //    if (other.TryGetComponent<IDamageable>(out var dmg))
        //        dmg.TakeDamage(data.attackPower);

        //    DestroyProjectile(other.ClosestPoint(transform.position));
        //}
    }

    //�ӽ� �ڵ�, �¾��� ��� ����Ʈ ȿ��
    private void DestroyProjectile(Vector3 hitPosition)
    {
        // �浹 ����Ʈ ����
        if (data.impactEffect != null)
            Instantiate(data.impactEffect, hitPosition, Quaternion.identity);

        Destroy(gameObject);
    }
}
