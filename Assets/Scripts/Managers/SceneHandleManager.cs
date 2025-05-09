using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum SCENE_TYPE
{
    LobbyScene,
    LoadingScene,
    DungeonScene
}
public class SceneHandleManager : Singleton<SceneHandleManager>
{
    private string nextSceneName;
    public string NextSceneName{ get => nextSceneName; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
     
    public void LoadScene(string sceneName)
    {
        // ���� ���� �̸� ���� ��Ʈ������ �����ϰ�
        nextSceneName = sceneName;
        // �ε�â�� ȣ��
        SceneManager.LoadScene(SCENE_TYPE.LoadingScene.ToString());
    }
}
