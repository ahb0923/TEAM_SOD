using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class InterActionZone : BaseInterAction
{
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private Animator buttonAnimator_blue;

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
        model.ButtonActions["Button_Enter"] = () =>
        {

            SceneHandleManager.Instance.LoadScene(SCENE_TYPE.DungeonScene.ToString());
            buttonAnimator.SetTrigger("Press");
        };
       
        model.ButtonActions["Button_Cancel"] = () =>
        {
            UIManager.Instance.ClosePanel(model);
            buttonAnimator_blue.SetTrigger("Press_blue");
        };



        UIManager.Instance.ShowPanel(model);
    }
    
   
}
