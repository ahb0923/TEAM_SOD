using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("스폰 포인트들")]
    [SerializeField] private Transform[] spawnPoints;
    
    [Header("맵 보상")]
    [SerializeField] private int rewardGold;
    [SerializeField] private int monsterCount;

    [Header("다음 맵 참조 (optional)")]
    [SerializeField] private Map nextMap;

    /// <summary>
    /// 외부에서 필요한 데이터 세팅 (ScriptableObject 등)
    /// </summary>
    public void Initialize(/* MapData data */)
    {
        // 예: monsterCount = data.monsterCount;
        // rewardGold = data.rewardGold;
    }

    /// <summary>
    /// 맵에 몬스터들을 일괄 스폰
    /// </summary>
    public void SpawnMonsters()
    {
        for (int i = 0; i < monsterCount && i < spawnPoints.Length; i++)
        {
           // MonsterData mData = DataManager.Instance.GetRandomMonsterData();
          //  Monster m = Instantiate(mData.Prefab, spawnPoints[i].position, Quaternion.identity);
          //  m.Initialize(mData);
           // m.OnDeath += CheckClear;
        }
    }

    /// <summary>
    /// 남은 몬스터가 0인지 확인 후 클리어 처리
    /// </summary>
    private void CheckClear()
    {
       // if (GameObject.FindObjectsOfType<Monster>().Length == 0)
           // OnCleared?.Invoke();
    }

    /// <summary>
    /// 맵 클리어 이벤트
    /// </summary>
  //  public event Action OnCleared;
}
