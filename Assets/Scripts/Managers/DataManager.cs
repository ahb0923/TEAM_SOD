using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("에디터 할당: 모든 무기 데이터 리스트")]
    [SerializeField] private List<WeaponData> weaponDataList;

    /*
    [Header("에디터 할당 : 모든 몬스터 데이터 리스트")]
    [SerializeField] private List<MonsterData> MonsterDataList;*/

    [Header("에디터 할당 : 모든 발사체 데이터 리스트")]
    [SerializeField] private List<ProjectileData> ProjectileDataList;

    private Dictionary<string, WeaponData> weaponDataMap;


    //무기 데이터를 반환하는 메소드
    public WeaponData GetWeaponData(string weaponId)
    {
        //if (weaponDataMap.TryGetValue(weaponId, out var data))
        //    return data;

        //Debug.LogError($"WeaponData not found: {weaponId}");
        return null;
    }
}
