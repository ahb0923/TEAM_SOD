using UnityEngine;


public class DungeonInitializer : MonoBehaviour
{
    [Header("맵 타일")]
    [SerializeField] private GameObject mapTilePrefab;
    [SerializeField] public Transform[] tilePositions;  

    private void Start()
    {
        foreach (var pos in tilePositions)
        {
            // 1) 타일 인스턴스화
            GameObject tileGO = Instantiate(
                mapTilePrefab,
                pos.position,
                pos.rotation,
                transform
            );

            // 2) MapTile 스크립트 꺼내서 랜덤 스폰
            Maptile tile = tileGO.GetComponent<Maptile>();
            if (tile != null)
                tile.SpawnMonstersRandomly();
           
        }
    }
}
