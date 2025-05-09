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




    public void OnTriggerEnter2D(Collider2D other)
    {
        if (hasSpawned || !other.CompareTag("Player"))
            return;



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
        hasSpawned = true;

    }
}