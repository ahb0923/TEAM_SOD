using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseData : ScriptableObject
{
    [SerializeField]
    public string dataKey;
    [SerializeField]
    public int code;
    [SerializeField]
    public GameObject prefab;
    
    // 초기 생성할 프리팹의 갯수
    [SerializeField]
    public int initSize = 10;
}
