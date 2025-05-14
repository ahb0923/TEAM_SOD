using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{


    //������� ���� ����� ����
    //�ִϸ��̼ǿ�
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

    [SerializeField] private DungeonRewardHandler rewardHandler;

    public void OnWaveClear()
    {
        rewardHandler.ShowRewardOptions();
    }

    protected override void Awake()
    {
        // 1) ���� �ε�� ������ ���� ����
        base.Awake();
        //CreateMonster();
        CreateMap();
        GameObject doorObj = GameObject.Find("Door");
        doorAnimator = doorObj.GetComponent<Animator>();
    }



    // �ִϸ��̼ǿ� 
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
            Debug.LogWarning($"i�� üũ : {i}");

            if (this.transform.Find("DungeonMaps") != null)
            {
                Debug.Log("��ġ");
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
            Debug.Log("5���̺� ���� Ŭ����");
            mapHandlers[currentDungeonCode].IsClear = true;
            return;
        }

        // ���� �� ��Ȱ��ȭ
        mapHandlers[CurrentDungeonCode].gameObject.SetActive(false);

        // ���� �� Ȱ��ȭ + ���̺� ����
        mapHandlers[nextCode].gameObject.SetActive(true);
        mapHandlers[nextCode].StartWaveFlow();

        CurrentDungeonCode = nextCode;
    }

    public Transform GetMapSpawnTransform(int mapCode)
    {
        return mapHandlers[mapCode].transform;
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
