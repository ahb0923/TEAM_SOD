using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ProjectileData")]
public class ProjectileData : WeaponData
{
  
    public GameObject prefab;         // �߻�ü ������
    public float moveSpeed;        // �ʱ� �ӵ�
    public float lifetime;         // ���� �ð�
    public  Color Color;
    //public LayerMask targetLayerMask; // ����� �� ��� (�÷��̾�/����)
    public GameObject impactEffect;   // �浹 ����Ʈ
}
