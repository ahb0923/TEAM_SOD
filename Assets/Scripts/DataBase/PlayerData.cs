using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private float hp;
    [SerializeField]
    private float maxHp;
    [SerializeField]
    private float atk;
    [SerializeField]
    private float def;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float gold;
    [SerializeField]
    private float _crit_Chance = 0;
    [SerializeField]
    private float _crit_Multiply = 0;
    [SerializeField]
    private float _in_invinsible_duration = 0;
    [SerializeField]
    private bool _is_invinsible;
}
