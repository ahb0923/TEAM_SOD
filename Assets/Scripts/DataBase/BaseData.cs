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
    
    // �ʱ� ������ �������� ����
    [SerializeField]
    public int initSize = 10;
}
