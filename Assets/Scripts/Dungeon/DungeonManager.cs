using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{

    //������� ���� ����� ����
    //�ִϸ��̼ǿ�
    [SerializeField] public bool checkClear=false;
    [SerializeField] private Animator doorAnimator;

    [SerializeField] private GameObject[] MapPrefab;
    //[SerializeField] private GameObject[] MonsterPrefab;
    [SerializeField] private GameObject[] selectPanel;

    private List<Transform> tilePositions = new List<Transform>();
    private List<GameObject> monsterList = new List<GameObject>();



    private void Awake()
    {
        // 1) ���� �ε�� ������ ���� ����
        CreateMap();
        //CreateMonster();

        GameObject doorObj = GameObject.Find("Door");
        doorAnimator = doorObj.GetComponent<Animator>();
    }

    private void Start()
    {
        Transform spawnTransform = tilePositions[0].Find("Grid/Spawn");

        if (spawnTransform != null)
        {
            Debug.Log($"ã���� : {tilePositions[0].Find("Grid/Spawn").name}");
            StartSpawn(spawnTransform, "Melee_Test", 3f, 10);  // 3�� ����, 10���� ����
            StartSpawn(spawnTransform, "Range_Test", 5f, 5);  // 5�� ����, 5���� ����
        }
        else
        {
            Debug.Log("���� ��ġ ����");
        }
    }
    // �ִϸ��̼ǿ� 
    private void Update()
    {

        if (checkClear == true)
        {
            doorAnimator.SetBool("Open",true);
        }
        else
        {
            doorAnimator.SetBool("Open", false);
        }
    }
    public void CreateMap()
    {
        for (int i = 0; i < MapPrefab.Length; i++)
        {
            Vector3 posA = new Vector3(i * 20, 0f, 0f);
            GameObject map = Instantiate(MapPrefab[i], posA, Quaternion.identity);
            tilePositions.Add(map.transform);
            map.transform.SetParent(this.transform);

        }

    }

    public void CreateMonster(Transform spawnTrans, string monsterKey)
    {
        BoxCollider2D spawnArea = spawnTrans.GetComponent<BoxCollider2D>();
        if (spawnArea == null)
        {
            Debug.Log("���������� �ö��̴� ����");
            return;
        }
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.size;

        float randX = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float randZ = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);
        float y = center.y;

        Vector3 spawnPos = new Vector3(randX, y, randZ);

        GameObject monster = PoolManager.Instance.GetObject(monsterKey, spawnPos);
        if (monster == null)
        {
            Debug.LogWarning($"Ǯ���� '{monsterKey}'�� �������� ���߽��ϴ�.");
            return;
        }

        monster.transform.SetParent(spawnTrans); // ���� ���� ����
        monsterList.Add(monster);
            /*
            for (int j = 0; j < MonsterPrefab.Length; j++)
            {
                // (2) �ະ�� min/max ������ ����
                float randX = Random.Range(posMin.x, posMax.x);
                float randY = Random.Range(posMin.y, posMax.y);

                // ���� ��ǥ
                Vector3 posC = new Vector3(randX, randY, posStandard.z);

                GameObject monster = Instantiate(MonsterPrefab[j], posC, Quaternion.identity);
                monster.transform.SetParent(tilePositions[j]);

                monsterList.Add(monster);

            }*/
    }

    public void StartSpawn(Transform spawnTrans, string monsterKey, float interval, int repeatCount = -1)
    {
        StartCoroutine(MonsterSpawnRoutine(spawnTrans, monsterKey, interval, repeatCount));
    }

    private IEnumerator MonsterSpawnRoutine(Transform spawnTrans, string monsterKey, float interval, int repeatCount)
    {
        int spawnCount = 0;

        while (repeatCount < 0 || spawnCount < repeatCount)
        {
            CreateMonster(spawnTrans, monsterKey);

            spawnCount++;
            yield return new WaitForSeconds(interval);
        }
    }


















































    //[Header("���� Ŭ���� �� ��� UI �г�")]
    //[SerializeField] private GameObject clearUIPanel;


    //[Header("�÷��̾� ��Ʈ�ѷ� (Inspector�� �Ҵ�)")]
    //[SerializeField] private MonoBehaviour playerController;
    //private int remainingMonsters = 0;



    //private void Awake()
    //{



    //    // UI�� ������ �� ���� ����
    //    if (clearUIPanel != null)
    //        clearUIPanel.SetActive(false);
    //}

    ///// <summary>�� Ÿ���� ���͸� �ϳ� ������ ������ ȣ��</summary>
    //public void RegisterMonster()
    //{
    //    remainingMonsters++;
    //}

    ///// <summary>���Ͱ� ���� ������ ȣ��</summary>
    //public void UnregisterMonster()
    //{
    //    remainingMonsters--;
    //    if (remainingMonsters <= 0)
    //    {
    //        ShowClearUI();
    //    }
    //}

    //private void ShowClearUI()
    //{

    //    clearUIPanel.SetActive(true);

    //}

    //public void Continue()
    //{
    //    // 1) UI �����
    //    clearUIPanel.SetActive(false);
    //    // 2) �÷��̾� ��Ʈ�� ����
    //    playerController.enabled = true;
    //    // 3) ���� ��(�Ǵ� �κ�)���� �̵�
    //    SceneHandleManager.Instance.LoadScene("LobbyScene");
    //}

    //============================
    //[Header("=== UI ���� ===")]
    //[SerializeField] private DungeonUI dungeonUI;
    //[SerializeField] private BossDungeonUI bossDungeonUI;
    //[SerializeField] private FailedDungeonUI failedDungeonUI;
    //[SerializeField] private StatController playerStat;
}
