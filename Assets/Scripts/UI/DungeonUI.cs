using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DungeonUI : BaseInterAction
{
    //[Header("클리어 보상 설정")]
    ////[SerializeField] private int clearGold = 100;

    //private RangeWeapon weapon; //플레이어 정보 
    //private GameObject Player;


    ////Awake로 해도...?(해야?)
    //private void Start()
    //{
    //    Player = GetComponent<GameObject>();
    //}
    //public override void OpenPanel()
    //{
    //    // 1) 무기 정보 가져오기  
    //    weapon = Player.GetComponentInChildren<RangeWeapon>();




    //    // 1) PanelModel 준비
    //    var model = new PanelModel
    //    {
    //        Panel = selectPanel,
    //        Sprites = new Dictionary<string, Sprite>(sprites),
    //        TextPro = new Dictionary<string, TextMeshProUGUI>(texts),
    //        ButtonActions = new Dictionary<string, UnityAction>()
    //    };

    //    // 2) 동적 텍스트 갱신
    //    //model.TextPro["AttackValueText"].text = player.Attack.ToString();
    //    //model.TextPro["AttackSpeedValueText"].text = player.AttackSpeed.ToString();
    //    //model.TextPro["MoveSpeedValueText"].text = player.MoveSpeed.ToString();
    //    //model.TextPro["ClearGoldText"].text = clearGold.ToString();
    //    //model.TextPro["CurrentMoneyText"].text = player.Money.ToString();


    //    // 3) 버튼 콜백 설정
    //    model.ButtonActions["Button_NextWave"] = () =>
    //    {
    //        selectPanel.SetActive(false);
             
    //       // MapManager.Instance.NextMap();
    //    };
    //    model.ButtonActions["UpgradeAttackBtn"] = () => { weapon.dungeon_AddPower++; }; 
    //    model.ButtonActions["UpgradeAttackBtn"] = () => { weapon.dungeon_AddSpeed++; }; 
    //    model.ButtonActions["UpgradeAttackBtn"] = () => { weapon.dungeon_ShotCount++; }; 
        




    //    // 4) UI 표시
    //    UIManager.Instance.ShowPanel(model);
    //}

}
