using UnityEngine;


public class DungeonInitializer : MonoBehaviour
{
    [Header("�� Ÿ��")]
    [SerializeField] private GameObject mapTilePrefab;
    [SerializeField] public Transform[] tilePositions;  

    private void Start()
    {
        foreach (var pos in tilePositions)
        {
            // 1) Ÿ�� �ν��Ͻ�ȭ
            GameObject tileGO = Instantiate(
                mapTilePrefab,
                pos.position,
                pos.rotation,
                transform
            );

            // 2) MapTile ��ũ��Ʈ ������ ���� ����
            Maptile tile = tileGO.GetComponent<Maptile>();
            if (tile != null)
                tile.SpawnMonstersRandomly();
           
        }
    }
}
