using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BossDungeonUI : BaseInterAction
{
    [Header("클리어 보상 설정")]
    [SerializeField] private int clearGold = 1000;

    [SerializeField] private StatController statController;

    
    public override void OpenPanel()
    {
        statController.GoldChangeApply(clearGold);

        var model = new PanelModel
        {
            Panel = selectPanel,
            Sprites = new Dictionary<string, Sprite>(sprites),
            TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
            ButtonActions = new Dictionary<string, UnityAction>
            {
                ["Button_Lobby"] = () => SceneHandleManager.Instance.LoadScene(SCENE_TYPE.LobbyScene.ToString())
            }
        };

        model.TextPro["ClearGoldValue"].text = clearGold.ToString();
        
        UIManager.Instance.ShowPanel(model);
    }
}
