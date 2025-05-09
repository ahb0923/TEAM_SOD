using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //발사체 발사 로직(발사체 생성과 생성할 발사체 프리팹 설정

   
    public static ProjectileManager Instance { get; private set; }
    [SerializeField] private ParticleSystem impactParticleSystem;

   private BasicBow b;//임시
    public Transform dummyTarget;

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
        // Inspector에 할당하지 않았다면, 씬에 있는 BasicBow를 찾아서 할당
        //이후 게임 씬에서 플레이어의 Weapon 오브젝트를 찾아서 할당하도록
        if (b == null)
            b = FindObjectOfType<BasicBow>();
    }
    void Update()
    {
  
            b.Attack(dummyTarget.position);//테스트용
    }

    public ProjectileController SpawnProjectile(ProjectileData data, Vector2 position, Vector2 direction)
    {
        var go = Instantiate(data.prefab, position, Quaternion.identity);
        var ctrl = go.GetComponent<ProjectileController>();
        ctrl.Initialize(data, direction);
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
