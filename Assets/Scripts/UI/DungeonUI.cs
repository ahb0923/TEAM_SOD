using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DungeonUI : BaseInterAction
{
    [Header("Ŭ���� ���� ����")]
    [SerializeField] private int clearGold = 100;

    private BaseWeapon weapon; //�÷��̾� ���� 

    public override void OpenPanel()
    {
        // 1) �÷��̾� ���� ��������  
        //weapon = GameManager.Instance.GetPlayerWeapon();


        // 0) Ŭ���� ��� ����
        //player.Money += clearGold;
        //UIManager.Instance.UpdateMoney(_player.Money);

        // 1) PanelModel �غ�
        var model = new PanelModel
        {
            Panel = selectPanel,
            Sprites = new Dictionary<string, Sprite>(sprites),
            TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
            ButtonActions = new Dictionary<string, UnityAction>()
        };

        // 2) ���� �ؽ�Ʈ ����
        //model.TextPro["AttackValueText"].text = player.Attack.ToString();
        //model.TextPro["AttackSpeedValueText"].text = player.AttackSpeed.ToString();
        //model.TextPro["MoveSpeedValueText"].text = player.MoveSpeed.ToString();
        //model.TextPro["ClearGoldText"].text = clearGold.ToString();
        //model.TextPro["CurrentMoneyText"].text = player.Money.ToString();

        // 3) ��ư �ݹ� ����
        model.ButtonActions["CloseButton"] = () =>
        {
            selectPanel.SetActive(false);
             
           // MapManager.Instance.NextMap();
        };
        //model.ButtonActions["UpgradeAttackBtn"] = () => { weapon.attackPower++; OpenPanel(); };
        //model.ButtonActions["UpgradeAtkSpdBtn"] = () => { weapon.AttackSpeed++; OpenPanel(); };
        //model.ButtonActions["UpgradeMoveSpdBtn"] = () => { weapon.MoveSpeed++; OpenPanel(); };

        // 4) UI ǥ��
        UIManager.Instance.ShowPanel(model);
    }

}
