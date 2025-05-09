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
        // 다음 씬의 이름 값을 스트링으로 저장하고
        nextSceneName = sceneName;
        // 로딩창을 호출
        SceneManager.LoadScene(SCENE_TYPE.LoadingScene.ToString());
    }
}
