using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string id; //if : enum ��� �ĺ��� ��� ���
    [Header("���� ���� ����")]
    public float delay = 1f;
    public float weaponSize = 1f;
    public float AttackPower = 1f;
    public float AttackSpeed = 1f;
    public float AttackRange = 10f;
   

}
