using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    //발사체 관련 처리
    //생성과 생명주기 후 파괴 처리
    [SerializeField] private LayerMask levelCollisionLayer;

    //private BasicBow BasicBow;
    //private float currentDuration;
    //private Vector2 direction;
    //private bool isReady;
    //private Transform pivot;

    //private Rigidbody2D _rigidbody;
    //private SpriteRenderer spriteRenderer;

    //public bool fxOnDestory = true;

    //private ProjectileManager projectileManager;

    //private void Awake()
    //{
    //    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    //    _rigidbody = GetComponent<Rigidbody2D>();
    //    pivot = transform.GetChild(0);
    //}
    //private void Update()
    //{
    //    if (!isReady)
    //    {
    //        return;
    //    }

    //    currentDuration += Time.deltaTime;

    //    //시간에 따른 화살 오브젝트 파괴
    //    //if (currentDuration > BasicBow.duration)
    //    //{
    //    //    DestroyProjectile(transform.position, false);
    //    //}

    //    _rigidbody.velocity = direction * BasicBow.Speed;
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
    //    {
    //        DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
    //    }
    //    else if (BasicBow.target.value == (BasicBow.target.value | (1 << collision.gameObject.layer)))
    //    {
    //        //
    //        DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
    //    }
    //}

    //public void Init(Vector2 direction, BasicBow basicBow, ProjectileManager projectileManager)
    //{
    //    this.projectileManager = projectileManager;

    //    BasicBow = basicBow;

    //    this.direction = direction;
    //    currentDuration = 0;
    //    transform.localScale = Vector3.one * basicBow.bulletSize;
    //    spriteRenderer.color = basicBow.projectileColor;

    //    transform.right = this.direction;

    //    if (this.direction.x < 0)
    //        pivot.localRotation = Quaternion.Euler(180, 0, 0);
    //    else
    //        pivot.localRotation = Quaternion.Euler(0, 0, 0);

    //    isReady = true;
    //}

    //private void DestroyProjectile(Vector3 position, bool createFx)
    //{
    //    if (createFx)
    //    {
    //        projectileManager.CreateImpactParticlesAtPostion(position, BasicBow);
    //    }

    //    Destroy(this.gameObject);
    //}
}
