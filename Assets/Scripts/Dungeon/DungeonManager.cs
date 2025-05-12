using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{

    //������� ���� ����� ����

    [SerializeField] private GameObject[] MapPrefab;
    [SerializeField] private GameObject[] MonsterPrefab;
    [SerializeField] private GameObject[] selectPanel;

    private List<Transform> tilePositions = new List<Transform>();
    private List<GameObject> monsterList = new List<GameObject>();



    private void Awake()
    {
        // 1) ���� �ε�� ������ ���� ����
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
                // (2) �ະ�� min/max ������ ����
                float randX = Random.Range(posMin.x, posMax.x);
                float randY = Random.Range(posMin.y, posMax.y);

                // ���� ��ǥ
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


}
