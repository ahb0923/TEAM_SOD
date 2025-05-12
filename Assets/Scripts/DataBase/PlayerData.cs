using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public float maxHp;
    [SerializeField]
    public float atk;
    [SerializeField]
    public float def;
    [SerializeField]
    public float moveSpeed;
    [SerializeField]
    public int gold;
    [SerializeField]
    public float _crit_Chance = 0;
    [SerializeField]
    public float _crit_Multiply = 0;
    [SerializeField]
    public float _in_invinsible_duration = 0;
    [SerializeField]
    public bool _is_invinsible;
}
