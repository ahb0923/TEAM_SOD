using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageInfo
{
    public float Attack { get; }
    public float Critcal_Chance { get; }

    public float Critical_Multiply { get; }

}