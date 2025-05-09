using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RangeWeaponData")]
public class RangeWeaponData : WeaponData
{
    [Header("����ü ����")]
    public GameObject projectilePrefab;// �߻��� ������
    public float bulletSize = 1f; //ũ��
    public float duration = 2f; // ���� �ð�
    public float spread = 0.1f; // Ȯ��
    public int numberOfProjectilesPerShot = 1; //����ü ��
    public float multipleProjectilesAngle = 15f; //���� �߻�ü ����
    public Color projectileColor = Color.white;

    [Header("�˹� ����")]
    public bool isOnKnockback = false;
    public float knockbackPower = 0.1f;
    public float knockbackTime = 0.5f;
}
