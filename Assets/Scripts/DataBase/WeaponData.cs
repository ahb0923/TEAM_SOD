using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("����")]
    public string id;
    public float attackPower;     // ����������ü ������ ����
    public float attackSpeed;     // �ʴ� ���� Ƚ��
    public float attackRange;     // Ÿ�� �Ǵ� ��Ÿ�
    public float weaponSize;
    public LayerMask layer;

    [Header("����ü �ɼ� (��������� null)")]
    public ProjectileData projectileData;
   
   // public int continuousShotCount;   // ���� �� (1�̸� �ܹ�)
    public int multiShotCount;        // �� ���� ��� ȭ�� ��
    public float multiShotAngle;        // ȭ�� ���� ����

    [Header("���� �� �߰� �ɷ�ġ (�⺻���� 0)")]
    public float dungeon_AddPower;
    public float dungeon_AddSpeed;
    public float dungeon_AddRange;
    public int dungeon_ShotCount;
}
