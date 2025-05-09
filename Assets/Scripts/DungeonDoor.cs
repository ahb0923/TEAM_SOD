using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{

    [Header("�÷��̾ �̵��� ��ǥ ���� ����Ʈ")]
    [SerializeField] private Transform[] playerTargets;


    int current = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        if (current < playerTargets.Length)
        {
            if (other.transform.position.x > playerTargets[current].position.x)
            {

                current++;


            }

            other.transform.position = playerTargets[current].position;
        }
        else
        {
            
            SceneHandleManager.Instance.LoadScene("LobbyScene");
            current = 0;


        }
    }

}
