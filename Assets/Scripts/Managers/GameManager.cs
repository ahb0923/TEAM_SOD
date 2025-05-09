using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private SceneHandleManager _sceneManager;
    [SerializeField] private UIManager _uiManager;






    #region SceneHandleManager

    public void LoadScene(string sceneName)
    {
        // 다음 씬의 이름 값을 스트링으로 저장하고
        _sceneManager.LoadScene(sceneName);
    }
}


