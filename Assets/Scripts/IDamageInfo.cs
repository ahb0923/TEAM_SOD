using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageInfo
{
    public float Attack { get; }
    public float Critcal_Chance { get; }
    public float Critical_Multiply { get; }
    // 뭐 추후 속성공격이나 관통같은게 필요하다면 여기서 확장이 가능할지도
}