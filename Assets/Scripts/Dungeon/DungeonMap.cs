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


    public void Init()
    {
        monsterList = new List<Monster>();
    }



    public void MonsterFactory()
    {

    }
}
