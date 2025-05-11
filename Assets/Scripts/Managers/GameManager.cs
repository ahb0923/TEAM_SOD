using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //인스펙터 창에서 직접 끌어다 attach
    [SerializeField] private BasePlayer player;

}
