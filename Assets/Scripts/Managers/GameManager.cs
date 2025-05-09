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
        // ���� ���� �̸� ���� ��Ʈ������ �����ϰ�
        _sceneManager.LoadScene(sceneName);
    }
}


