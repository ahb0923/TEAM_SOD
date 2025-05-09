using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Maptile : MonoBehaviour
{
    [Header("스폰할 몬스터 프리팹 목록")]
    [SerializeField] private GameObject[] monsterPrefabs;

    [Header("타일당 스폰할 몬스터 수")]
    [SerializeField] private int monstersPerTile = 3;

    private BoxCollider2D spawnArea;

    private void Awake()
    {
        spawnArea = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        // 인스턴스화 후 한 번만 랜덤 스폰
        SpawnMonstersRandomly();
    }

    public void SpawnMonstersRandomly()
    {
        if (spawnArea == null)
        {
            
            return;
        }

        Bounds b = spawnArea.bounds;
        for (int i = 0; i < monstersPerTile; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(b.min.x, b.max.x),
                Random.Range(b.min.y, b.max.y)
            );

            var prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
            Instantiate(prefab, pos, Quaternion.identity, transform);
        }
    }
}