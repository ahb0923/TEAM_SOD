using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //상점, 인벤토리 등 아이템 데이터를 관리하는 매니저
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
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);

        //    weaponDataMap = new Dictionary<string, WeaponData>();
        //    foreach (var data in weaponDataList)
        //        weaponDataMap[data.id] = data;
        //}
        //else Destroy(gameObject);
    }

    //무기 데이터를 반환하는 메소드
    public WeaponData GetWeaponData(string weaponId)
    {
        //if (weaponDataMap.TryGetValue(weaponId, out var data))
        //    return data;

        //Debug.LogError($"WeaponData not found: {weaponId}");
        return null;
    }
}
