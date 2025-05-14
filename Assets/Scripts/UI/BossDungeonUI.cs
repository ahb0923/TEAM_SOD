using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BossDungeonUI : BaseInterAction
{
    
    [SerializeField] private int clearGold = 1000;

    private GameObject player;
    private StatController statController;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        // StatController วาด็ 

        statController = player.GetComponent<StatController>();

        
    }
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
