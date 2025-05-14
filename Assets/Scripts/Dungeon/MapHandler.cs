using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    private const int WaveNum = 5;
    private const int InitCount = 2;

    //[SerializeField] private GameObject[] monsterPrefabs;

    // �ش� �ʿ��� ��ȯ�� ��� ���͸� ��� �ִ� �����̳�
    private List<Dictionary<MONSTER_KEY, int>> monsterContainer;

    // �̹� ���̺꿡���� ����� ���͵��� ������Ʈ �����̳�
    private List<GameObject> monsterList = new List<GameObject>();

    [SerializeField]
    private int mapCode;
    public int MapCode
    {
        get => mapCode;
        set => mapCode = value;
    }

    [SerializeField]
    private MapData mapData;

    [Header("�ڷ�ƾ ���� ����")]
    [SerializeField]
    private bool isRunning = false;

    [Header("���� ���� Ŭ���� �ǵ� ����")]
    [SerializeField]
    private bool isClear;
    public bool IsClear { get; set; }

    [Header("���� ���̺�")]
    [SerializeField]
    private int currWave;

    private Transform spawnTransform;
    private bool isInitialized = false;

    public void Init()
    {
        currWave = 1;
        isClear = false;
        spawnTransform = transform.Find("Grid/Spawn");
        if (spawnTransform == null)
        {
            Debug.LogError($"{name} - SpawnTransform ã�� ����! Grid/Spawn ��θ� Ȯ���ϼ���.");
        }
        monsterContainer = new();
        MONSTER_KEY[] monsterKeys = (MONSTER_KEY[])Enum.GetValues(typeof(MONSTER_KEY));

        for (int i = 0; i < WaveNum; i++)
        {
            Dictionary<MONSTER_KEY, int> waveData = new Dictionary<MONSTER_KEY, int>();

            // �� ���̺긶�� ��ȯ �� ���� ���� >> Ŀ���� ����� ��������� �ٸ� ����� ���ϸ� ��
            // ����� �ܼ��ϰ� 1���̺� :  2+1�� ����, ���Ÿ� ���� 3������ ��ȯ �� ���� 
            int count = InitCount + currWave;

            // �� ���ͺ��� �ٸ� ������ŭ �����ϰ� �ʹٸ� ���⼭ ��Ʈ��, ���� �����ϴ°� ������ ��
            foreach (MONSTER_KEY key in monsterKeys)
            {
                waveData.Add(key, count);
            }
            if (waveData == null) { Debug.LogWarning("���̺� ������ ����!"); }
            monsterContainer.Add(waveData);
        }
        Debug.Log($"���� �����̳ʻ����� {monsterContainer.Count}");
    }

    private void Start()
    {

    }
    private void Update()
    {
        //Debug.Log($"���� ����Ʈ ���� : {monsterList.Count}");
    }
    /*
    private void OnEnable()
    {
        if (!isInitialized)
        {
            Init();  // ���⿡ �ű�
            isInitialized = true;
        }

        StartWaveFlow();
    }*/
    public void StartWaveFlow()
    {
        if (isRunning) 
            return;

        if (DungeonManager.Instance == null)
        {
            Debug.LogWarning("dm�Ŵ��� ����");
            return;
        }

        if (DungeonManager.Instance.CurrentDungeonCode != MapCode)
        {
            Debug.Log($"�� üũ(mapCode: {MapCode}, currentDungeon: {DungeonManager.Instance.CurrentDungeonCode})");
            return;
        }

        isRunning = true;
        // ���� ������ ��ġ�ϴ��� Ȯ���� �Ǿ��ٸ� �ڷ�ƾ ���� ����
        StartCoroutine(WaveFlowRoutine());
    }

    private IEnumerator WaveFlowRoutine()
    {
        // ���̺� ���� �ִϸ��̼��� �ְ� �ʹٸ� �� ��ġ���� yield return WaitForSeconds(float) �̿��Ͽ� �ִϸ��̼� �ð���ŭ ����
        while (currWave <= WaveNum)
        {
            // ���� �������� �� ���̺� ���۸��� Ȯ��
            while (DungeonManager.Instance.CurrentDungeonCode != MapCode)
            {
                yield return null;
            }
            
            // ���� �ϰ� ��ȯ  => ���� ��ȯ ����̶� ��� �ǰ�..
            StartSpawnBatch(spawnTransform, currWave-1);

            yield return new WaitUntil(() => IsAllMonstersDead());

            yield return new WaitUntil(() => IsRewardSelected());

            // 2�� ��� => �ð� ������ ���� ���� �帧 üũ�غ��� ���� ����
            yield return new WaitForSeconds(2f);
            currWave++;
        }

        yield return new WaitUntil(() => IsRewardSelected());

        //�ڵ����� ���� �̵��ȴٸ� ���⼭, ���� �� �ö��̴��� �΋H���� �̵��Ѵٸ� DungeonManager���� �ڵ鸵
        //DungeonManager.Instance.NextMap();

        yield break;
    }
    private bool IsAllMonstersDead()
    {
        monsterList.RemoveAll(m => m == null);  // ���ŵ� ������Ʈ ����
        return monsterList.Count == 0;
    }
    private bool IsRewardSelected()
    {
        // ���� ���� ���� �Ϸ� ���� �Ǵ� �ڵ� �ۼ��� ��
        // ��...DungeonUI�� Reward UI ������Ʈ�� ���ù�ư�� ������ OnDisable�� �ɴ� �ش� �Լ��� ȣ���Ѵٴ���..
        return true;
    }
    // ������ �ö��̴� �ڽ� �ȿ� ���� ������ġ ����
    public void CreateMonster(Transform spawnTrans, string monsterKey)
    {
        BoxCollider2D spawnArea = spawnTrans.GetComponent<BoxCollider2D>();
        if (spawnArea == null)
        {
            Debug.Log("���������� �ö��̴� ����");
            return;
        }
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.size;

        float randX = UnityEngine.Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float randZ = UnityEngine.Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);
        float y = center.y;

        Vector3 spawnPos = new Vector3(randX, y, randZ);

        GameObject monster = PoolManager.Instance.GetObject(monsterKey, spawnPos);
        if (monster == null)
        {
            Debug.LogWarning($"Ǯ���� '{monsterKey}'�� �������� ���߽��ϴ�.");
            return;
        }

        monster.transform.SetParent(spawnTrans); // ���� ���� ����
        monsterList.Add(monster);
    }

    // ���� ��ȯ�ÿ� ���
    public void StartSpawnDelay(Transform spawnTrans, string monsterKey, float interval, int repeatCount = -1)
    {
        StartCoroutine(MonsterSpawnRoutine(spawnTrans, monsterKey, interval, repeatCount));
    }
    private IEnumerator MonsterSpawnRoutine(Transform spawnTrans, string monsterKey, float interval, int repeatCount)
    {
        int spawnCount = 0;

        while (repeatCount < 0 || spawnCount < repeatCount)
        {
            CreateMonster(spawnTrans, monsterKey);

            spawnCount++;
            yield return new WaitForSeconds(interval);
        }
    }

    // �ϰ� ��ȯ�ÿ� ���
    public void StartSpawnBatch(Transform spawnTrans, int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= monsterContainer.Count)
        {
            Debug.LogWarning($"���̺� �ε���: {waveIndex}");
            return;
        }

        Dictionary<MONSTER_KEY, int> waveData = monsterContainer[waveIndex];

        // kvp = key value pair
        foreach (var kvp in waveData)
        {
            MONSTER_KEY key = kvp.Key;
            int count = kvp.Value;

            for (int i = 0; i < count; i++)
            {
                CreateMonster(spawnTrans, key.ToString());
            }
        }
    }

    // ���� Monster ��ũ��Ʈ�� OnDeath �̺�Ʈ�� ������, �ش� ��ũ��Ʈ�� Die()�� OnDestroy() �ȿ� DungeonManager.Instance.UnregisterMonster(); �� ���� �־� �ּ���.


    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (hasSpawned || !other.CompareTag("Player")) return;
    //    hasSpawned = true;

    //    Bounds b = spawnArea.bounds;
    //    for (int i = 0; i < monstersPerTile; i++)
    //    {
    //        Vector2 pos = new Vector2(
    //            Random.Range(b.min.x, b.max.x),
    //            Random.Range(b.min.y, b.max.y)
    //        );
    //        // 1) ���� �ν��Ͻ�ȭ
    //        var go = Instantiate(
    //            monsterPrefabs[Random.Range(0, monsterPrefabs.Length)],
    //            pos,
    //            Quaternion.identity,
    //            transform
    //        );

    //        // 2) DungeonManager�� ���
    //       // DungeonManager.Instance.RegisterMonster();

    //        // 3) ���� ���� �̺�Ʈ ����
    //        var monster = go.GetComponent<Monster>();
    //        if (monster != null)
    //        {
    //            // ���Ͱ� ���� ������ DungeonManager�� ��� ����
    //        }
    //        else
    //        {
    //            // Monster ��ũ��Ʈ�� ���ٸ�, ��� �±� ������� OnDestroy���� ó���ϰ� �� ���� �ֽ��ϴ�.
    //        }
    //    }
    //}
}