using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    [Header("�÷��̾ �̵��� ��ǥ ���� ����Ʈ")]
    [SerializeField] private Transform[] playerTargets;

    // ������ �̵��� �ε���
    private int current = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Ÿ���� �����Ǿ� ���� �ʰų�, ��� ������ ����
        if (playerTargets == null || playerTargets.Length == 0)
            return;

        // 1) ���� ������ Ÿ�ٿ� �������� �ʾҴٸ�
        if (current < playerTargets.Length)
        {
            // ���� �̵�
            other.transform.position = playerTargets[current].position;
            current++;
        }

        // 2) ���� �ε����� �迭 ���� �̻��̸� �κ��
        if (current >= playerTargets.Length)
        {
            SceneHandleManager.Instance.LoadScene("LobbyScene");
            current = 0;  // �ʿ��ϴٸ� �ε��� �ʱ�ȭ
        }
    }

}
