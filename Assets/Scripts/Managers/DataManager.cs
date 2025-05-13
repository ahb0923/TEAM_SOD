using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Header("������ �Ҵ�: ��� ���� ������ ����Ʈ")]
    [SerializeField] private List<WeaponData> weaponDataList;

    /*
    [Header("������ �Ҵ� : ��� ���� ������ ����Ʈ")]
    [SerializeField] private List<MonsterData> MonsterDataList;*/

    [Header("������ �Ҵ� : ��� �߻�ü ������ ����Ʈ")]
    [SerializeField] private List<ProjectileData> ProjectileDataList;

    private Dictionary<string, WeaponData> weaponDataMap;


    //���� �����͸� ��ȯ�ϴ� �޼ҵ�
    public WeaponData GetWeaponData(string weaponId)
    {
        //if (weaponDataMap.TryGetValue(weaponId, out var data))
        //    return data;

        //Debug.LogError($"WeaponData not found: {weaponId}");
        return null;
    }
}
