using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NpcInterAction : BaseInterAction
{

    private int _beforeCount = 1;
    private int _afterCount = 2;


    protected int lastMoney;
    protected int goldPrice=100;


    public  override void OpenPanel()
    {
        var model = new PanelModel
        {
            Panel = selectPanel,
            Sprites = new Dictionary<string, Sprite>(sprites),
            TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
            ButtonActions = new Dictionary<string, UnityAction>(),

        };

        
        model.TextPro["Before"].text = _beforeCount.ToString();
        model.TextPro["After"].text = _afterCount.ToString();
        model.TextPro["Gold"].text = goldPrice.ToString();


        



     
        model.ButtonActions["Button_UpGrade"] = () => 
        {
            _beforeCount++;
            _afterCount++;
           
            lastMoney -= goldPrice;
            goldPrice += 100;
            //무기 강화 로직

            OpenPanel();
        };
        model.ButtonActions["Button_Cancel"] = () =>
        {
            UIManager.Instance.ClosePanel(model);
        };



        UIManager.Instance.ShowPanel(model);

    }


}
