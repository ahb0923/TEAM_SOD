using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("공통")]
    public string id;
    public float attackPower;     // 근접·투사체 데미지 공통
    public float attackSpeed;     // 초당 공격 횟수
    public float attackDelay;     // 공격 애니메이션 딜레이
    public float attackRange;     // 타격 또는 사거리

    [Header("투사체 옵션 (근접무기는 null)")]
    public ProjectileData projectileData;
   
    public int continuousShotCount;   // 연사 수 (1이면 단발)
    public int multiShotCount;        // 한 번에 쏘는 화살 수
    public float multiShotAngle;        // 화살 퍼짐 각도

    // 타입 넣기 -> DataManager에서 다루기 위해
}
