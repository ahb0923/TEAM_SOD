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
    public float weaponSize;
    public LayerMask layer;

    [Header("투사체 옵션 (근접무기는 null)")]
    public ProjectileData projectileData;
   
   // public int continuousShotCount;   // 연사 수 (1이면 단발)
    public int multiShotCount;        // 한 번에 쏘는 화살 수
    public float multiShotAngle;        // 화살 퍼짐 각도

    [Header("던전 내 추가 능력치 (기본값은 0)")]
    public float dungeon_AddPower;
    public float dungeon_AddSpeed;
    public float dungeon_AddRange;
    public int dungeon_ShotCount;
}
