using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{


    public TextMeshProUGUI dugeonEnterUI;
    public  void ShowUI()
    {
        
    }

    public void DugeonEnterUI ()
    {
        dugeonEnterUI.gameObject.SetActive(true);
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
