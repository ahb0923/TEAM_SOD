using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBtn : MonoBehaviour
{
    public void EnterDungeonButton()
    {
        SceneHandleManager.Instance.LoadScene(SCENE_TYPE.DungeonScene.ToString());
    }

    public void ExitDungeonButton()
    {
        SceneHandleManager.Instance.LoadScene(SCENE_TYPE.LobbyScene.ToString());
    }
}
