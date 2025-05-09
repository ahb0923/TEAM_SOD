using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    
    [SerializeField] private UIManager _uiManager;


    public void ShowUI() 
    { 
      _uiManager.ShowUI();


    }

    //public void DugeonEnterUI()
    //{
    //    _uiManager.DugeonEnterUI();
    //}




}


