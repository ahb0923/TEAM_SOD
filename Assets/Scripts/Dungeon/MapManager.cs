using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    //[Header("�Ϲ� �� ������ Ǯ")]
    //[SerializeField] private GameObject[] roomPrefabs;
    //[Header("���� �� ������")]
    //[SerializeField] private GameObject bossRoomPrefab;
    //[Header("���� �� �� (���� ����)")]
    //[SerializeField] private int dungeonLength = 5;

    //[Header("�÷��̾� Transform")]
    //[SerializeField] private Transform playerTransform;
    //[Header("ī�޶� Follow ������Ʈ")]
    //[SerializeField] private CameraFollow cameraFollow;

    //private List<GameObject> dungeonSequence;
    //private int currentIndex;
    //private GameObject currentMapInstance;
    //void Start()
    //{
    //    GenerateSequence();
    //    // ù �� �ε�
    //    LoadMap(0);
    //}
    //private void GenerateSequence()
    //{
    //    dungeonSequence = new List<GameObject>();
    //    for (int i = 0; i < dungeonLength; i++)
    //    {
    //        var prefab = roomPrefabs[Random.Range(0, roomPrefabs.Length)];
    //        dungeonSequence.Add(prefab);
    //    }
    //    dungeonSequence.Add(bossRoomPrefab);
    //}
    //public void LoadMap(int index)
    //{
    //    // ���� �� ����
    //    if (currentMapInstance != null)
    //        Destroy(currentMapInstance);

    //    // �ε��� ��� �˻�
    //    if (index < 0 || index >= dungeonSequence.Count)
    //        return;

    //    currentIndex = index;
    //    // �� �ν��Ͻ�ȭ
    //    currentMapInstance = Instantiate(dungeonSequence[index]);

       
        
    //    var spawn = currentMapInstance.transform.Find("PlayerSpawn");
    //    if (spawn != null)
    //        playerTransform.position = spawn.position;
    //    cameraFollow.target = playerTransform;

    //    var tiles = currentMapInstance.GetComponentsInChildren<Maptile>(true);
    //    int totalMonsters = tiles.Sum(t => t.monsterPrefabs.Length);
    //    DungeonManager.Instance.Init(totalMonsters);


    //    foreach (var tile in tiles)
    //        tile.Initialize();
    //}

    //public void NextMap()
    //{
    //    int next = currentIndex + 1;
    //    if (next < dungeonSequence.Count)
    //        LoadMap(next);
    //    else
    //        SceneHandleManager.Instance.LoadScene("LobbyScene");
    //}
}
