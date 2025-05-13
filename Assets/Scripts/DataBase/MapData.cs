using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MapData")]

public class MapData : ScriptableObject
{
    [SerializeField]
    private int code;
    [SerializeField]
    private int monsterWave;
    [SerializeField]
    private DUNGEON_TYPE type;
}
