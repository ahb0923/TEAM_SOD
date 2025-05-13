using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


enum DUNGEON_TYPE
{
    NORMAL,
    BOSS
}
public class DungeonMap : MonoBehaviour
{
    private List<Monster> monsterList;


    private void Awake()
    {
        monsterList = new List<Monster>();
    }


    public void Init()
    {
    }



    public void MonsterFactory()
    {

    }
}
