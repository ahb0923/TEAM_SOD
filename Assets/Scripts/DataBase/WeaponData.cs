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
    public float attackDelay;     // ���� �ִϸ��̼� ������
    public float attackRange;     // Ÿ�� �Ǵ� ��Ÿ�

    [Header("����ü �ɼ� (��������� null)")]
    public ProjectileData projectileData;
   
    public int continuousShotCount;   // ���� �� (1�̸� �ܹ�)
    public int multiShotCount;        // �� ���� ��� ȭ�� ��
    public float multiShotAngle;        // ȭ�� ���� ����

    // Ÿ�� �ֱ� -> DataManager���� �ٷ�� ����
}
