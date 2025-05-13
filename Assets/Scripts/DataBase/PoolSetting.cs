using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pool_Name", menuName = "Pooling/PoolSetting")]
public class PoolSetting : ScriptableObject
{
    [Header("풀링 리스트")]
    public List<BaseData> datas;
}
