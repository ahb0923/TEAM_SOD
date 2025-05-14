using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NpcInterAction : BaseInterAction
{

    public  override void OpenPanel()
    {
        var model = new PanelModel
        {
            Panel = selectPanel,
            Sprites = new Dictionary<string, Sprite>(sprites),
            TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
            ButtonActions = new Dictionary<string, UnityAction>(),

        };

        model.ButtonActions["Button_Enter"] = () =>
        {

        };

        UIManager.Instance.ShowPanel(model);

    }


}
