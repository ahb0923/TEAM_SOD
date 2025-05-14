using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_AllDeathBtn : MonoBehaviour
{
    [SerializeField]
    private Transform monsterPool; 

    public void OnCliCKDeath()
    {
        Transform parentPool = monsterPool.GetChild(DungeonManager.Instance.CurrentDungeonCode);
        Transform currPool = monsterPool.GetChild(DungeonManager.Instance.CurrentDungeonCode).Find("Grid/Spawn").gameObject.transform;
        if (currPool == null)
        {
            Debug.Log("몬스터풀 못찾음.");
            return;
        }

        Debug.Log(currPool.name);
        if (currPool.childCount == 0)
        {
            Debug.Log("몬스터가 없음");
            return;
        }

        for (int i = currPool.childCount - 1; i >= 0; i--)
        {
            Transform child = currPool.GetChild(i);

            var pooled = child.GetComponent<StatController>();
            if (pooled != null)
            {
                PoolManager.Instance.ReturnObject(pooled.testkey, child.gameObject);

                // 임시 제거 로직
                parentPool.GetComponent<MapHandler>().RemoveMonster(child.gameObject);
            }
            else
            {
                Debug.LogWarning($"{child.gameObject.name}에 PooledMonster 스크립트가 없습니다.");
            }   
        }
        Debug.Log(currPool.childCount);
    }



}
