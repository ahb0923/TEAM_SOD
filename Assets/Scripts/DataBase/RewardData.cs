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
    [Header("데이터 기본 속성")]
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

    [Header("플레이어 증감 수치")]
    [SerializeField]
    public float hp;        // 이 부분은 힐 느낌 으로 쓸려고 합니다
    [SerializeField]
    public float maxHp;     // 이 부분은 최대 체력 느낌 증강
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

    [Header("무기 증감 수치")]
    [SerializeField]
    public float weaponPower;
    [SerializeField]
    public float weaponSpeed;
    [SerializeField]
    public float weaponRange;
    [SerializeField]
    public int weaponShotCount;
}
