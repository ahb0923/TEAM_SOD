using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageInfo
{
    public float Attack { get; set; }
    public float Critcal_Chance { get; set; }
    public float Critical_Multiply { get; set; }
    // 뭐 추후 속성공격이나 관통같은게 필요하다면 여기서 확장이 가능할지도
}