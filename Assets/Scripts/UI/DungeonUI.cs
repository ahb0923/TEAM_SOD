using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DungeonUI : BaseInterAction
{
    //[Header("Ŭ���� ���� ����")]
    ////[SerializeField] private int clearGold = 100;

    //private RangeWeapon weapon; //�÷��̾� ���� 
    //private GameObject Player;


    ////Awake�� �ص�...?(�ؾ�?)
    //private void Start()
    //{
    //    Player = GetComponent<GameObject>();
    //}
    //public override void OpenPanel()
    //{
    //    // 1) ���� ���� ��������  
    //    weapon = Player.GetComponentInChildren<RangeWeapon>();




    //    // 1) PanelModel �غ�
    //    var model = new PanelModel
    //    {
    //        Panel = selectPanel,
    //        Sprites = new Dictionary<string, Sprite>(sprites),
    //        TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
    //        ButtonActions = new Dictionary<string, UnityAction>()
    //    };

    //    // 2) ���� �ؽ�Ʈ ����
    //    //model.TextPro["AttackValueText"].text = player.Attack.ToString();
    //    //model.TextPro["AttackSpeedValueText"].text = player.AttackSpeed.ToString();
    //    //model.TextPro["MoveSpeedValueText"].text = player.MoveSpeed.ToString();
    //    //model.TextPro["ClearGoldText"].text = clearGold.ToString();
    //    //model.TextPro["CurrentMoneyText"].text = player.Money.ToString();


    //    // 3) ��ư �ݹ� ����
    //    model.ButtonActions["Button_NextWave"] = () =>
    //    {
    //        selectPanel.SetActive(false);
             
    //       // MapManager.Instance.NextMap();
    //    };
    //    model.ButtonActions["UpgradeAttackBtn"] = () => { weapon.dungeon_AddPower++; }; 
    //    model.ButtonActions["UpgradeAttackBtn"] = () => { weapon.dungeon_AddSpeed++; }; 
    //    model.ButtonActions["UpgradeAttackBtn"] = () => { weapon.dungeon_ShotCount++; }; 
        




    //    // 4) UI ǥ��
    //    UIManager.Instance.ShowPanel(model);
    //}

}
