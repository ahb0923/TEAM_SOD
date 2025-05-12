using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //발사체 발사 매니저(발사체 생성과 생성할 발사체 프리팹 설정

   
    public static ProjectileManager Instance { get; private set; }
    [SerializeField] private ParticleSystem impactParticleSystem; //파티클 임시


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        
    }
    void Update()
    {
   
    }

    //투사체 생성
    public ProjectileController SpawnProjectile(ProjectileData data, Vector2 position, Vector2 direction, float atk)
    {
        var go = Instantiate(data.prefab, position, Quaternion.identity);
        var ctrl = go.GetComponent<ProjectileController>();
        ctrl.Initialize(data, direction, atk);
        return ctrl;
    }

    //파티클 함수(임시)
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
