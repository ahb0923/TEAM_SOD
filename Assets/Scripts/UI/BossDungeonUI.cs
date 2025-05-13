using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BossDungeonUI : BaseInterAction
{
    public override void OpenPanel()
    {
        var model = new PanelModel
        {
            Panel = selectPanel,
            Sprites = new Dictionary<string, Sprite>(sprites),
            TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
            ButtonActions = new Dictionary<string, UnityAction>
            {
                ["Button_Lobby"] = () => SceneHandleManager.Instance.LoadScene(SCENE_TYPE.DungeonScene.ToString())
            }
        };
        UIManager.Instance.ShowPanel(model);
    }
}
