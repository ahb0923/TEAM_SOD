using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    //[Header("일반 방 프리팹 풀")]
    //[SerializeField] private GameObject[] roomPrefabs;
    //[Header("보스 방 프리팹")]
    //[SerializeField] private GameObject bossRoomPrefab;
    //[Header("던전 방 수 (보스 제외)")]
    //[SerializeField] private int dungeonLength = 5;

    //[Header("플레이어 Transform")]
    //[SerializeField] private Transform playerTransform;
    //[Header("카메라 Follow 컴포넌트")]
    //[SerializeField] private CameraFollow cameraFollow;

    //private List<GameObject> dungeonSequence;
    //private int currentIndex;
    //private GameObject currentMapInstance;
    //void Start()
    //{
    //    GenerateSequence();
    //    // 첫 맵 로드
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
    //    // 기존 맵 제거
    //    if (currentMapInstance != null)
    //        Destroy(currentMapInstance);

    //    // 인덱스 경계 검사
    //    if (index < 0 || index >= dungeonSequence.Count)
    //        return;

    //    currentIndex = index;
    //    // 맵 인스턴스화
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
