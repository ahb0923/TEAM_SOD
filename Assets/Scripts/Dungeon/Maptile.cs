using System.Threading;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Maptile : MonoBehaviour
{
    [Header("스폰할 몬스터 목록")]
    [SerializeField] private GameObject[] monsterPrefabs;

    [Header("스폰할 몬스터 수")]
    [SerializeField] private int monstersPerTile;




    private bool hasSpawned = false;

    private BoxCollider2D spawnArea;
    



    private void Awake()
    {

        spawnArea = GetComponent<BoxCollider2D>();
        spawnArea.isTrigger = true;
    }

   // 만약 Monster 스크립트가 OnDeath 이벤트가 없으면, 해당 스크립트의 Die()나 OnDestroy() 안에 DungeonManager.Instance.UnregisterMonster(); 를 직접 넣어 주세요.


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned || !other.CompareTag("Player")) return;
        hasSpawned = true;

        Bounds b = spawnArea.bounds;
        for (int i = 0; i < monstersPerTile; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(b.min.x, b.max.x),
                Random.Range(b.min.y, b.max.y)
            );
            // 1) 몬스터 인스턴스화
            var go = Instantiate(
                monsterPrefabs[Random.Range(0, monsterPrefabs.Length)],
                pos,
                Quaternion.identity,
                transform
            );

            // 2) DungeonManager에 등록
            DungeonManager.Instance.RegisterMonster();
            
            // 3) 몬스터 죽음 이벤트 연결
            var monster = go.GetComponent<Monster>();
            if (monster != null)
            {
                // 몬스터가 죽을 때마다 DungeonManager에 등록 해제
            }
            else
            {
                // Monster 스크립트가 없다면, 대신 태그 기반으로 OnDestroy에서 처리하게 할 수도 있습니다.
            }
        }
    }
}