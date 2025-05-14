using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    private DungeonManager dungeonManager;

    private void Awake()
    {
        dungeonManager = FindObjectOfType<DungeonManager>();
        if (dungeonManager == null)
            Debug.LogError("DungeonManager�� ã�� �� �����ϴ�!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dungeonManager.checkClear = true;
            dungeonManager.NextMap();
        }
    }


}
