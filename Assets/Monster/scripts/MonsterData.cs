using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data")]
public class MonsterData : ScriptableObject
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


}
