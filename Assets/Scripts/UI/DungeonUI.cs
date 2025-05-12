using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DungeonUI : BaseInterAction
{
    [Header("클리어 보상 설정")]
    [SerializeField] private int clearGold = 100;

    private BaseWeapon weapon; //플레이어 정보 

    public override void OpenPanel()
    {
        // 1) 플레이어 정보 가져오기  
        //weapon = GameManager.Instance.GetPlayerWeapon();


        // 0) 클리어 골드 지급
        //player.Money += clearGold;
        //UIManager.Instance.UpdateMoney(_player.Money);

        // 1) PanelModel 준비
        var model = new PanelModel
        {
            Panel = selectPanel,
            Sprites = new Dictionary<string, Sprite>(sprites),
            TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
            ButtonActions = new Dictionary<string, UnityAction>()
        };

        // 2) 동적 텍스트 갱신
        //model.TextPro["AttackValueText"].text = player.Attack.ToString();
        //model.TextPro["AttackSpeedValueText"].text = player.AttackSpeed.ToString();
        //model.TextPro["MoveSpeedValueText"].text = player.MoveSpeed.ToString();
        //model.TextPro["ClearGoldText"].text = clearGold.ToString();
        //model.TextPro["CurrentMoneyText"].text = player.Money.ToString();

        // 3) 버튼 콜백 설정
        model.ButtonActions["CloseButton"] = () =>
        {
            selectPanel.SetActive(false);
             
           // MapManager.Instance.NextMap();
        };
        //model.ButtonActions["UpgradeAttackBtn"] = () => { weapon.attackPower++; OpenPanel(); };
        //model.ButtonActions["UpgradeAtkSpdBtn"] = () => { weapon.AttackSpeed++; OpenPanel(); };
        //model.ButtonActions["UpgradeMoveSpdBtn"] = () => { weapon.MoveSpeed++; OpenPanel(); };

        // 4) UI 표시
        UIManager.Instance.ShowPanel(model);
    }

}
