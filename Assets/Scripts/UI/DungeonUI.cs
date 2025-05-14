using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DungeonUI : BaseInterAction
{
  

    private RangeWeapon weapon; //플레이어 정보 
    private GameObject Player;


    //Awake로 해도...?(해야?)
    private void Start()
    {
        Player = GameObject.FindWithTag("Player");
        weapon = Player.GetComponentInChildren<RangeWeapon>();
    }
    public override void OpenPanel()
    {
   

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
        

        // 3) 버튼 콜백 설정
        
        //model.ButtonActions["Button_AtkPower"] = () => { weapon.data.dungeon_AddPower++; selectPanel.SetActive(false); };  // MapManager.Instance.NextMap();
        //model.ButtonActions["Button_AtkSpeed"] = () => { weapon.data.dungeon_AddSpeed++; selectPanel.SetActive(false); };
        //model.ButtonActions["Button_ShotCount"] = () => { weapon.data.dungeon_ShotCount++; selectPanel.SetActive(false); };





        // 4) UI 표시
        UIManager.Instance.ShowPanel(model);
    }

}
