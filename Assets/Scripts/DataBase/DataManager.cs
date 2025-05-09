using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    [Header("에디터 할당: 모든 무기 데이터 리스트")]
    [SerializeField] private List<WeaponData> weaponDataList;

    [Header("에디터 할당 : 모든 몬스터 데이터 리스트")]
    [SerializeField] private List<MonsterData> MonsterDataList;

    [Header("에디터 할당 : 모든 발사체 데이터 리스트")]
    [SerializeField] private List<ProjectileData> ProjectileDataList;

    private Dictionary<string, WeaponData> weaponDataMap;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            weaponDataMap = new Dictionary<string, WeaponData>();
            foreach (var data in weaponDataList)
                weaponDataMap[data.id] = data;
        }
        else Destroy(gameObject);
    }

    public WeaponData GetWeaponData(string weaponId)
    {
        if (weaponDataMap.TryGetValue(weaponId, out var data))
            return data;

        Debug.LogError($"WeaponData not found: {weaponId}");
        return null;
    }
}
