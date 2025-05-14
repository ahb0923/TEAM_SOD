using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField]
    public PoolSetting[] poolSetting;

    private Dictionary<string, Queue<GameObject>> pools = new();
    private Dictionary<string, GameObject> prefabMap = new();

    protected override void Awake()
    {
        base.Awake();
        InitPool();
    }

    private void InitPool()
    {
        for (int j = 0; j < poolSetting.Length; j++)
        {
            foreach (var data in poolSetting[j].datas)
            {
                if (data.prefab == null)
                    Debug.Log($"해당 『{data.dataKey}』의 프리팹 연결 안되었음");

                string key = data.name;
                GameObject prefab = data.prefab;
                int count = data.initSize;

                prefabMap[key] = prefab;
                Queue<GameObject> queue = new Queue<GameObject>();

                for (int i = 0; i < count; i++)
                {
                    var obj = Instantiate(prefab, transform);
                    obj.SetActive(false);
                    queue.Enqueue(obj);
                }

                pools[key] = queue;
                Debug.Log($"풀링 체크용 : {data.dataKey}");
            }
        }
    }

    public GameObject GetObject(string key, Vector3 pos)
    {
        if (!pools.ContainsKey(key))
        {
            Debug.Log($"키 존재x : {key}");
            return null;
        }

        // 
        GameObject obj = pools[key].Count > 0 ? pools[key].Dequeue() : Instantiate(prefabMap[key], transform);
        obj.transform.position = pos;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(string key, GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.transform, worldPositionStays: false);
        pools[key].Enqueue(obj);
    }
    
    //public void ReturnProjectile(string key, GameObject obj)
    //{
    //    obj.SetActive(false);
    //    obj.transform.SetParent(transform);
    //    pools[key].Enqueue(obj);
    //}

}
