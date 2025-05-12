using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{

    //빈공간에 맵을 만드는 로직

    [SerializeField] private GameObject[] MapPrefab;
    [SerializeField] private GameObject[] MonsterPrefab;
    [SerializeField] private GameObject[] selectPanel;

    private List<Transform> tilePositions = new List<Transform>();
    private List<GameObject> monsterList = new List<GameObject>();



    private void Awake()
    {
        // 1) 씬이 로드될 때마다 맵을 생성
        CreateMap();
        CreateMonster();
    }

    private void Update()
    {
       
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

    public void CreateMonster()
    {
        for (int i = 0; i < tilePositions.Count; i++)
        {
            Vector3 posStandard = tilePositions[i].position;
            Vector3 posMin = posStandard + new Vector3(-7.5f, -4.0f, 0f);
            Vector3 posMax = posStandard + new Vector3(7.5f, 4.0f, 0f);


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

            }
        }

    }

    public void RemainMonser() 
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            if (monsterList[i] == null)
            {
               
            }
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


}
