using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("���� ����Ʈ��")]
    [SerializeField] private Transform[] spawnPoints;
    
    [Header("�� ����")]
    [SerializeField] private int rewardGold;
    [SerializeField] private int monsterCount;

    [Header("���� �� ���� (optional)")]
    [SerializeField] private Map nextMap;

    /// <summary>
    /// �ܺο��� �ʿ��� ������ ���� (ScriptableObject ��)
    /// </summary>
    public void Initialize(/* MapData data */)
    {
        // ��: monsterCount = data.monsterCount;
        // rewardGold = data.rewardGold;
    }

    /// <summary>
    /// �ʿ� ���͵��� �ϰ� ����
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
    /// ���� ���Ͱ� 0���� Ȯ�� �� Ŭ���� ó��
    /// </summary>
    private void CheckClear()
    {
       // if (GameObject.FindObjectsOfType<Monster>().Length == 0)
           // OnCleared?.Invoke();
    }

    /// <summary>
    /// �� Ŭ���� �̺�Ʈ
    /// </summary>
  //  public event Action OnCleared;
}
