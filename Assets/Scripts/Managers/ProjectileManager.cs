using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    //�߻�ü �߻� �Ŵ���(�߻�ü ������ ������ �߻�ü ������ ����

   
    public static ProjectileManager Instance { get; private set; }
    [SerializeField] private ParticleSystem impactParticleSystem; //��ƼŬ �ӽ�

    private RangeWeapon b;//�ӽ�
    public Transform dummyTarget;//�׽�Ʈ�� Ÿ�� ������Ʈ

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
        //�Ǵ� ���͵� �����ϰ�
        if (b == null)
            b = FindObjectOfType<RangeWeapon>();
    }
    void Update()
    {
        //�׽�Ʈ�� -> �Ʒ� �Լ��� �÷��̾ ���� ���� ������ �ۼ��Ͻø� �˴ϴ�.
        // b.Attack(dummyTarget.position);
    }

    //����ü ����
    public ProjectileController SpawnProjectile(ProjectileData data, Vector2 position, Vector2 direction, float atk)
    {
        var go = Instantiate(data.prefab, position, Quaternion.identity);
        var ctrl = go.GetComponent<ProjectileController>();
        ctrl.Initialize(data, direction, atk);
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
