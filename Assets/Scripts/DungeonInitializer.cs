using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInitializer : MonoBehaviour
{
    [Header("맵 타일")]
    [SerializeField] private GameObject mapTilePrefab;          // MapTile 프리팹
    [SerializeField] private Transform[] tilePositions;         // 타일을 뿌릴 4개 위치

    [Header("스폰할 몬스터")]
    [SerializeField] private GameObject[] monsterPrefabs;       // 각 타일 위에 올릴 몬스터 프리팹 (tilePositions.Length와 동일 크기)

    private void Start()
    {
        // 배열 길이 체크
        if (tilePositions.Length != monsterPrefabs.Length)
        {
            Debug.LogError("tilePositions.Length와 monsterPrefabs.Length가 달라요!");
            return;
        }

        // 4개의 맵 타일과 몬스터 동시 배치
        for (int i = 0; i < tilePositions.Length; i++)
        {
            // 1) MapTile 인스턴스화
            GameObject tile = Instantiate(
                mapTilePrefab,
                tilePositions[i].position,
                tilePositions[i].rotation,
                transform    // 이 스크립트가 붙은 오브젝트의 자식으로 두면 정리하기 편함
            );

            // 2) 몬스터 인스턴스화
            //    (타일 중심에 스폰하거나, 오프셋이 필요하면 tilePositions[i].position + offset)
            Instantiate(
                monsterPrefabs[i],
                tile.transform.position,    // tile.transform 을 parent 로 해도 좋음
                Quaternion.identity,
                tile.transform
            );
        }
    }
}
