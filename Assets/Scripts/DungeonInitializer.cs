using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInitializer : MonoBehaviour
{
    [Header("�� Ÿ��")]
    [SerializeField] private GameObject mapTilePrefab;          // MapTile ������
    [SerializeField] private Transform[] tilePositions;         // Ÿ���� �Ѹ� 4�� ��ġ

    [Header("������ ����")]
    [SerializeField] private GameObject[] monsterPrefabs;       // �� Ÿ�� ���� �ø� ���� ������ (tilePositions.Length�� ���� ũ��)

    private void Start()
    {
        // �迭 ���� üũ
        if (tilePositions.Length != monsterPrefabs.Length)
        {
            Debug.LogError("tilePositions.Length�� monsterPrefabs.Length�� �޶��!");
            return;
        }

        // 4���� �� Ÿ�ϰ� ���� ���� ��ġ
        for (int i = 0; i < tilePositions.Length; i++)
        {
            // 1) MapTile �ν��Ͻ�ȭ
            GameObject tile = Instantiate(
                mapTilePrefab,
                tilePositions[i].position,
                tilePositions[i].rotation,
                transform    // �� ��ũ��Ʈ�� ���� ������Ʈ�� �ڽ����� �θ� �����ϱ� ����
            );

            // 2) ���� �ν��Ͻ�ȭ
            //    (Ÿ�� �߽ɿ� �����ϰų�, �������� �ʿ��ϸ� tilePositions[i].position + offset)
            Instantiate(
                monsterPrefabs[i],
                tile.transform.position,    // tile.transform �� parent �� �ص� ����
                Quaternion.identity,
                tile.transform
            );
        }
    }
}
