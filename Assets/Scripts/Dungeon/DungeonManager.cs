using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{


    //빈공간에 맵을 만드는 로직
    //애니매이션용
    [SerializeField] public bool checkClear = false;
    [SerializeField] private Animator doorAnimator;

    [SerializeField] private GameObject[] MapPrefab;
    //[SerializeField] private GameObject[] MonsterPrefab;
    [SerializeField] private GameObject[] selectPanel;

    [SerializeField]
    private int currentDungeonCode;
    public int CurrentDungeonCode { get; set; }

    private List<Transform> tilePositions = new List<Transform>();
    private List<MapHandler> mapHandlers = new();


    protected override void Awake()
    {
        // 1) 씬이 로드될 때마다 맵을 생성
        base.Awake();
        //CreateMonster();
        CreateMap();
        GameObject doorObj = GameObject.Find("Door");
        doorAnimator = doorObj.GetComponent<Animator>();

        if (rewardUI == null)
            Debug.LogWarning("보상 버튼 못찾음!!!!");
        if (rewardUI != null)
        {
            rewardUI.gameObject.SetActive(false);
        }

    }



    // 애니매이션용 
    private void Update()
    {
        if (checkClear == true)
        {
            doorAnimator.SetBool("Open", true);
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
            Vector3 posA = new Vector3(i * 40, 0f, 0f);

            GameObject map = Instantiate(MapPrefab[i], posA, Quaternion.identity);
            Debug.LogWarning($"i값 체크 : {i}");

            if (this.transform.Find("DungeonMaps") != null)
            {
                Debug.Log("위치");
                map.transform.SetParent(this.transform.Find("DungeonMaps").transform);
            }

            var mapHandler = map.GetComponent<MapHandler>();
            //mapHandler.Init();
            mapHandler.MapCode = i;

            tilePositions.Add(map.transform);
            mapHandlers.Add(mapHandler);
            mapHandler.Init();
            map.SetActive(i == 0);
        }
        CurrentDungeonCode = 0;
        mapHandlers[0].StartWaveFlow();
    }

    public void NextMap()
    {
        int nextCode = CurrentDungeonCode + 1;

        if (nextCode >= mapHandlers.Count)
        {
            Debug.Log("5웨이브 까지 클리어");
            mapHandlers[currentDungeonCode].IsClear = true;
            return;
        }

        // 현재 맵 비활성화
        mapHandlers[CurrentDungeonCode].gameObject.SetActive(false);

        // 다음 맵 활성화 + 웨이브 시작
        mapHandlers[nextCode].gameObject.SetActive(true);
        mapHandlers[nextCode].StartWaveFlow();

        CurrentDungeonCode = nextCode;
    }


    public Transform GetMapSpawnTransform(int mapCode)
    {
        return mapHandlers[mapCode].transform;
    }


    // 임시 보상 ui화면 띄우기

    [SerializeField]
    Transform rewardUI;
    private int selectedRewardIndex = -1;
    public bool RewardSelected => selectedRewardIndex != -1;

    public void ViewReawardButton()
    {
        selectedRewardIndex = -1;
        rewardUI.gameObject.SetActive(true);
    }

    public void SelectReward(int index)
    {
        selectedRewardIndex = index;
        rewardUI.gameObject.SetActive(false);
        Debug.Log($"보상 선택됨: {index}");
    }

    public int GetSelectedReward()
    {
        return selectedRewardIndex;
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
