using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/MeleeWeaponData")]
public class MeleeWeaponData : WeaponData
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;
    //근접 무기 정보 
}
