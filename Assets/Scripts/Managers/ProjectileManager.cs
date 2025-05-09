using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //발사체 발사 로직(발사체 생성과 생성할 발사체 프리팹 설정

    //private static ProjectileManager instance;
    //public static ProjectileManager Instance { get { return instance; } }
    //[SerializeField] private ParticleSystem impactParticleSystem;

    //private void Awake()
    //{
    //    instance = this;
    //}

    //public void ShootBullet(BasicBow basicBow, Vector2 startPostiion, Vector2 direction)
    //{
    //    GameObject origin = basicBow.projectilePrefab;
    //    GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

    //    ProjectileController projectileController = obj.GetComponent<ProjectileController>();
    //    projectileController.Init(direction, basicBow, this);
    //}

    ////파티클 함수(임시)
    //public void CreateImpactParticlesAtPostion(Vector3 position, BasicBow basicBow)
    //{
    //    impactParticleSystem.transform.position = position;
    //    ParticleSystem.EmissionModule em = impactParticleSystem.emission;
    //    em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(basicBow.bulletSize * 5)));
    //    ParticleSystem.MainModule mainModule = impactParticleSystem.main;
    //    mainModule.startSpeedMultiplier = basicBow.bulletSize * 10f;
    //    impactParticleSystem.Play();
    //}
}
