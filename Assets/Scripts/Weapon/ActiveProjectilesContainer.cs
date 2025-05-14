using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveProjectilesContainer : MonoBehaviour
{
    public static ActiveProjectilesContainer Instance { get; private set; }
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}
