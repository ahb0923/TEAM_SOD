using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public float spawnTime = 0;
    public PoolManager pool;
    private void Update()
    {
        spawnTime += Time.deltaTime;
        if(spawnTime > 3)
        {
            Spawn();
        }
    }

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        PoolManager.Instance.GetObject(MONSTER_KEY.Melee_Test.ToString(), transform.position);
        spawnTime = 0;
    }

}
