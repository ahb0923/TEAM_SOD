using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InterActionZone : BaseInterAction
{
    
    public override void OpenPanel()
    {
        var model = new PanelModel
        {
            Panel = selectPanel,
            Sprites = new Dictionary<string, Sprite>(sprites),
            TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
            ButtonActions = new Dictionary<string, UnityAction>(),
        };

        //  버튼 기능
        model.ButtonActions["OkButton"] = () =>
        {
            SceneHandleManager.Instance.LoadScene(SCENE_TYPE.DungeonScene.ToString());
        };
       
        model.ButtonActions["Button_Cancel"] = () =>
        {
            UIManager.Instance.ClosePanel(model);
        };



        UIManager.Instance.ShowPanel(model);
    }
    
   
}
