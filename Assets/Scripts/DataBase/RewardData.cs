using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum REWARD_GRADE
{
    NORMAL,
    RARE,
    UNIQUE
}
[CreateAssetMenu(menuName = "Data/RewardData")]
public class RewardData : ScriptableObject
{
    [Header("������ �⺻ �Ӽ�")]
    [SerializeField]
    public int code;
    [SerializeField]
    public REWARD_GRADE grade;
    [SerializeField]
    public string title;
    [SerializeField]
    public string text;
    [SerializeField]
    public Sprite image;

    [Header("�÷��̾� ���� ��ġ")]
    [SerializeField]
    public float hp;        // �� �κ��� �� ���� ���� ������ �մϴ�
    [SerializeField]
    public float maxHp;     // �� �κ��� �ִ� ü�� ���� ����
    [SerializeField]
    public int atk;
    [SerializeField]
    public int def;
    [SerializeField]
    public float playerMoveSpeed;
    [SerializeField]
    public float playerCriChance;
    [SerializeField]
    public float playerCriMulti;
    [SerializeField]
    public float playerInvisible;

    [Header("���� ���� ��ġ")]
    [SerializeField]
    public float weaponPower;
    [SerializeField]
    public float weaponSpeed;
    [SerializeField]
    public float weaponRange;
    [SerializeField]
    public int weaponShotCount;
}
