using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{

    //빈공간에 맵을 만드는 로직
    //애니매이션용
    [SerializeField] public bool checkClear=false;
    [SerializeField] private Animator doorAnimator;

    [SerializeField] private GameObject[] MapPrefab;
    //[SerializeField] private GameObject[] MonsterPrefab;
    [SerializeField] private GameObject[] selectPanel;

    private List<Transform> tilePositions = new List<Transform>();
    private List<GameObject> monsterList = new List<GameObject>();



    private void Awake()
    {
        // 1) 씬이 로드될 때마다 맵을 생성
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
            Debug.Log($"찾은거 : {tilePositions[0].Find("Grid/Spawn").name}");
            StartSpawn(spawnTransform, "Melee_Test", 3f, 10);  // 3초 간격, 10마리 생성
            StartSpawn(spawnTransform, "Range_Test", 5f, 5);  // 5초 간격, 5마리 생성
        }
        else
        {
            Debug.Log("스폰 위치 없음");
        }
    }
    // 애니매이션용 
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
            Debug.Log("스폰공간에 컬라이더 없음");
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
            Debug.LogWarning($"풀에서 '{monsterKey}'를 가져오지 못했습니다.");
            return;
        }

        monster.transform.SetParent(spawnTrans); // 구조 정리 목적
        monsterList.Add(monster);
            /*
            for (int j = 0; j < MonsterPrefab.Length; j++)
            {
                // (2) 축별로 min/max 순으로 랜덤
                float randX = Random.Range(posMin.x, posMax.x);
                float randY = Random.Range(posMin.y, posMax.y);

                // 월드 좌표
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


















































    //[Header("던전 클리어 시 띄울 UI 패널")]
    //[SerializeField] private GameObject clearUIPanel;


    //[Header("플레이어 컨트롤러 (Inspector에 할당)")]
    //[SerializeField] private MonoBehaviour playerController;
    //private int remainingMonsters = 0;



    //private void Awake()
    //{



    //    // UI는 시작할 때 숨겨 놓기
    //    if (clearUIPanel != null)
    //        clearUIPanel.SetActive(false);
    //}

    ///// <summary>맵 타일이 몬스터를 하나 스폰할 때마다 호출</summary>
    //public void RegisterMonster()
    //{
    //    remainingMonsters++;
    //}

    ///// <summary>몬스터가 죽을 때마다 호출</summary>
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
    //    // 1) UI 숨기기
    //    clearUIPanel.SetActive(false);
    //    // 2) 플레이어 컨트롤 복원
    //    playerController.enabled = true;
    //    // 3) 다음 씬(또는 로비)으로 이동
    //    SceneHandleManager.Instance.LoadScene("LobbyScene");
    //}

    //============================
    //[Header("=== UI 연결 ===")]
    //[SerializeField] private DungeonUI dungeonUI;
    //[SerializeField] private BossDungeonUI bossDungeonUI;
    //[SerializeField] private FailedDungeonUI failedDungeonUI;
    //[SerializeField] private StatController playerStat;
}
