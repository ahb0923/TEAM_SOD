using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RangeWeaponData")]
public class RangeWeaponData : WeaponData
{
    [Header("투사체 정보")]
    public GameObject projectilePrefab;// 발사할 프리팹
    public float bulletSize = 1f; //크기
    public float duration = 2f; // 지속 시간
    public float spread = 0.1f; // 확산
    public int numberOfProjectilesPerShot = 1; //투사체 수
    public float multipleProjectilesAngle = 15f; //다중 추사체 각도
    public Color projectileColor = Color.white;

    [Header("넉백 정보")]
    public bool isOnKnockback = false;
    public float knockbackPower = 0.1f;
    public float knockbackTime = 0.5f;
}
