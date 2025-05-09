using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public string id;
    public GameObject prefab;         // �߻�ü ������
    public float moveSpeed;        // �ʱ� �ӵ�
    public float damage;           // ���� ������
    public float lifetime;         // ���� �ð�
    public  Color Color;
    //public LayerMask targetLayerMask; // ����� �� ��� (�÷��̾�/����)
    public GameObject impactEffect;   // �浹 ����Ʈ
}
