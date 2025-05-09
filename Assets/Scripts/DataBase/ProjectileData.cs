using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ProjectileData")]
public class ProjectileData : WeaponData
{
  
    public GameObject prefab;         // 발사체 프리팹
    public float moveSpeed;        // 초기 속도
    public float lifetime;         // 생존 시간
    public  Color Color;
    //public LayerMask targetLayerMask; // 맞춰야 할 대상 (플레이어/몬스터)
    public GameObject impactEffect;   // 충돌 이펙트
}
