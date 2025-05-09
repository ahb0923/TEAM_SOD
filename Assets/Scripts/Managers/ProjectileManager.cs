using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //�߻�ü �߻� ����(�߻�ü ������ ������ �߻�ü ������ ����

   
    public static ProjectileManager Instance { get; private set; }
    [SerializeField] private ParticleSystem impactParticleSystem;

   private BasicBow b;//�ӽ�
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
        // Inspector�� �Ҵ����� �ʾҴٸ�, ���� �ִ� BasicBow�� ã�Ƽ� �Ҵ�
        //���� ���� ������ �÷��̾��� Weapon ������Ʈ�� ã�Ƽ� �Ҵ��ϵ���
        if (b == null)
            b = FindObjectOfType<BasicBow>();
    }
    void Update()
    {
  
            b.Attack(dummyTarget.position);//�׽�Ʈ��
    }

    public ProjectileController SpawnProjectile(ProjectileData data, Vector2 position, Vector2 direction)
    {
        var go = Instantiate(data.prefab, position, Quaternion.identity);
        var ctrl = go.GetComponent<ProjectileController>();
        ctrl.Initialize(data, direction);
        return ctrl;
    }

    //��ƼŬ �Լ�(�ӽ�)
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
