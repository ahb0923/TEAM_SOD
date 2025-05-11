using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerStat")]
public class PlayerData : ScriptableObject
{
    public float MaxHp;
    public float Atk;
    public float Def;
    public float MoveSpeed;
    public int Gold;
    public float CritChance;
    public float CritMultiply;
    public float InvinsibleDuration;
    public bool Is_Invinsible;
}
