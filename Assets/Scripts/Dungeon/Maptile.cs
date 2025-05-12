using System.Threading;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Maptile : MonoBehaviour
{
    [Header("������ ���� ���")]
    [SerializeField] private GameObject[] monsterPrefabs;

    [Header("������ ���� ��")]
    [SerializeField] private int monstersPerTile;




    private bool hasSpawned = false;

    private BoxCollider2D spawnArea;
    



    private void Awake()
    {

        spawnArea = GetComponent<BoxCollider2D>();
        spawnArea.isTrigger = true;
    }

   // ���� Monster ��ũ��Ʈ�� OnDeath �̺�Ʈ�� ������, �ش� ��ũ��Ʈ�� Die()�� OnDestroy() �ȿ� DungeonManager.Instance.UnregisterMonster(); �� ���� �־� �ּ���.


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
            // 1) ���� �ν��Ͻ�ȭ
            var go = Instantiate(
                monsterPrefabs[Random.Range(0, monsterPrefabs.Length)],
                pos,
                Quaternion.identity,
                transform
            );

            // 2) DungeonManager�� ���
            DungeonManager.Instance.RegisterMonster();
            
            // 3) ���� ���� �̺�Ʈ ����
            var monster = go.GetComponent<Monster>();
            if (monster != null)
            {
                // ���Ͱ� ���� ������ DungeonManager�� ��� ����
            }
            else
            {
                // Monster ��ũ��Ʈ�� ���ٸ�, ��� �±� ������� OnDestroy���� ó���ϰ� �� ���� �ֽ��ϴ�.
            }
        }
    }
}