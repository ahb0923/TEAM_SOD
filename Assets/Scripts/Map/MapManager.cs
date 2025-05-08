using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("맵 프리팹 리스트")]
    [SerializeField] private List<Map> mapPrefabs;

    private Map currentMap;

    public void LoadMap(int mapIndex)
    {
        UnloadCurrentMap();
        currentMap = Instantiate(mapPrefabs[mapIndex], transform);
       
        currentMap.Initialize();           // 데이터 세팅
        
        currentMap.SpawnMonsters();        // 몬스터 일괄 스폰
       
      //  OnMapLoaded?.Invoke(currentMap);
    }

    public void UnloadCurrentMap()
    {
        if (currentMap != null)
            Destroy(currentMap.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
