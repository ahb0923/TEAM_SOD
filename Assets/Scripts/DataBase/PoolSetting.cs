using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pool_Name", menuName = "Pooling/PoolSetting")]
public class PoolSetting : ScriptableObject
{
    [Header("Ǯ�� ����Ʈ")]
    public List<BaseData> datas;
}
