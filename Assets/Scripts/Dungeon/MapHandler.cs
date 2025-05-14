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

    // 해당 맵에서 소환될 모든 몬스터를 담고 있는 컨테이너
    private List<Dictionary<MONSTER_KEY, int>> monsterContainer;

    // 이번 웨이브에서만 사용할 몬스터들의 오브젝트 컨테이너
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

    [Header("코루틴 동작 여부")]
    [SerializeField]
    private bool isRunning = false;

    [Header("현재 던전 클리어 판독 여부")]
    [SerializeField]
    private bool isClear;
    public bool IsClear { get; set; }

    [Header("현재 웨이브")]
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
            Debug.LogError($"{name} - SpawnTransform 찾지 못함! Grid/Spawn 경로를 확인하세요.");
        }
        monsterContainer = new();
        MONSTER_KEY[] monsterKeys = (MONSTER_KEY[])Enum.GetValues(typeof(MONSTER_KEY));

        for (int i = 0; i < WaveNum; i++)
        {
            Dictionary<MONSTER_KEY, int> waveData = new Dictionary<MONSTER_KEY, int>();

            // 각 웨이브마다 소환 될 몬스터 숫자 >> 커스텀 밸류로 가고싶으면 다른 방식을 택하면 됨
            // 현재는 단순하게 1웨이브 :  2+1로 근접, 원거리 각각 3마리씩 소환 할 예정 
            int count = InitCount + currWave;

            // 각 몬스터별로 다른 갯수만큼 조절하고 싶다면 여기서 컨트롤, 구조 변경하는게 유리할 듯
            foreach (MONSTER_KEY key in monsterKeys)
            {
                waveData.Add(key, count);
            }
            if (waveData == null) { Debug.LogWarning("웨이브 데이터 없음!"); }
            monsterContainer.Add(waveData);
        }
        Debug.Log($"몬스터 컨테이너사이즈 {monsterContainer.Count}");
    }

    private void Start()
    {

    }
    private void Update()
    {
        //Debug.Log($"몬스터 리스트 갯수 : {monsterList.Count}");
    }
    /*
    private void OnEnable()
    {
        if (!isInitialized)
        {
            Init();  // 여기에 옮김
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
            Debug.LogWarning("dm매니저 없음");
            return;
        }

        if (DungeonManager.Instance.CurrentDungeonCode != MapCode)
        {
            Debug.Log($"맵 체크(mapCode: {MapCode}, currentDungeon: {DungeonManager.Instance.CurrentDungeonCode})");
            return;
        }

        isRunning = true;
        // 현재 던전과 일치하는지 확인이 되었다면 코루틴 동작 시작
        StartCoroutine(WaveFlowRoutine());
    }

    private IEnumerator WaveFlowRoutine()
    {
        // 웨이브 등장 애니메이션을 넣고 싶다면 이 위치에서 yield return WaitForSeconds(float) 이용하여 애니메이션 시간만큼 지연
        while (currWave <= WaveNum)
        {
            // 현재 던전인지 매 웨이브 시작마다 확인
            while (DungeonManager.Instance.CurrentDungeonCode != MapCode)
            {
                yield return null;
            }
            
            // 몬스터 일괄 소환  => 지연 소환 방식이랑 섞어도 되고..
            StartSpawnBatch(spawnTransform, currWave-1);

            yield return new WaitUntil(() => IsAllMonstersDead());

            yield return new WaitUntil(() => IsRewardSelected());

            // 2초 대기 => 시간 변경은 실제 게임 흐름 체크해보고 변경 ㄱㄱ
            yield return new WaitForSeconds(2f);
            currWave++;
        }

        yield return new WaitUntil(() => IsRewardSelected());

        //자동으로 맵이 이동된다면 여기서, 만약 문 컬라이더에 부딫혀서 이동한다면 DungeonManager에서 핸들링
        //DungeonManager.Instance.NextMap();

        yield break;
    }
    private bool IsAllMonstersDead()
    {
        monsterList.RemoveAll(m => m == null);  // 제거된 오브젝트 정리
        return monsterList.Count == 0;
    }
    private bool IsRewardSelected()
    {
        // 실제 보상 선택 완료 여부 판단 코드 작성할 것
        // 뭐...DungeonUI의 Reward UI 오브젝트가 선택버튼을 눌러서 OnDisable이 될대 해당 함수를 호출한다던지..
        return true;
    }
    // 정해전 컬라이더 박스 안에 몬스터 랜덤위치 생성
    public void CreateMonster(Transform spawnTrans, string monsterKey)
    {
        BoxCollider2D spawnArea = spawnTrans.GetComponent<BoxCollider2D>();
        if (spawnArea == null)
        {
            Debug.Log("스폰공간에 컬라이더 없음");
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
            Debug.LogWarning($"풀에서 '{monsterKey}'를 가져오지 못했습니다.");
            return;
        }

        monster.transform.SetParent(spawnTrans); // 구조 정리 목적
        monsterList.Add(monster);
    }

    // 지연 소환시에 사용
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

    // 일괄 소환시에 사용
    public void StartSpawnBatch(Transform spawnTrans, int waveIndex)
    {
        if (waveIndex < 0 || waveIndex >= monsterContainer.Count)
        {
            Debug.LogWarning($"웨이브 인덱스: {waveIndex}");
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

    // 만약 Monster 스크립트가 OnDeath 이벤트가 없으면, 해당 스크립트의 Die()나 OnDestroy() 안에 DungeonManager.Instance.UnregisterMonster(); 를 직접 넣어 주세요.


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
    //        // 1) 몬스터 인스턴스화
    //        var go = Instantiate(
    //            monsterPrefabs[Random.Range(0, monsterPrefabs.Length)],
    //            pos,
    //            Quaternion.identity,
    //            transform
    //        );

    //        // 2) DungeonManager에 등록
    //       // DungeonManager.Instance.RegisterMonster();

    //        // 3) 몬스터 죽음 이벤트 연결
    //        var monster = go.GetComponent<Monster>();
    //        if (monster != null)
    //        {
    //            // 몬스터가 죽을 때마다 DungeonManager에 등록 해제
    //        }
    //        else
    //        {
    //            // Monster 스크립트가 없다면, 대신 태그 기반으로 OnDestroy에서 처리하게 할 수도 있습니다.
    //        }
    //    }
    //}
}