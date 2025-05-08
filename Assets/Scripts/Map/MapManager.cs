using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("�� ������ ����Ʈ")]
    [SerializeField] private List<Map> mapPrefabs;

    private Map currentMap;

    public void LoadMap(int mapIndex)
    {
        UnloadCurrentMap();
        currentMap = Instantiate(mapPrefabs[mapIndex], transform);
       
        currentMap.Initialize();           // ������ ����
        
        currentMap.SpawnMonsters();        // ���� �ϰ� ����
       
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
