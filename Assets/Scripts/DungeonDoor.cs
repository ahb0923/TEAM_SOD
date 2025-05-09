using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    [Header("플레이어가 이동할 목표 지점 리스트")]
    [SerializeField] private Transform[] playerTargets;

    // 다음에 이동할 인덱스
    private int current = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // 타겟이 설정되어 있지 않거나, 비어 있으면 무시
        if (playerTargets == null || playerTargets.Length == 0)
            return;

        // 1) 아직 마지막 타겟에 도달하지 않았다면
        if (current < playerTargets.Length)
        {
            // 순간 이동
            other.transform.position = playerTargets[current].position;
            current++;
        }

        // 2) 다음 인덱스가 배열 길이 이상이면 로비로
        if (current >= playerTargets.Length)
        {
            SceneHandleManager.Instance.LoadScene("LobbyScene");
            current = 0;  // 필요하다면 인덱스 초기화
        }
    }

}
