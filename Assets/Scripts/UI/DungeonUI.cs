using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DungeonUI : BaseInterAction
{
  

    private RangeWeapon weapon; //�÷��̾� ���� 
    private GameObject Player;


    //Awake�� �ص�...?(�ؾ�?)
    private void Start()
    {
        Player = Player = GameObject.FindWithTag("Player");
        weapon = Player.GetComponentInChildren<RangeWeapon>();
    }
    public override void OpenPanel()
    {
   

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
        

        // 3) ��ư �ݹ� ����
        
        model.ButtonActions["Button_AtkPower"] = () => { weapon.data.dungeon_AddPower++; selectPanel.SetActive(false); };  // MapManager.Instance.NextMap();
        model.ButtonActions["Button_AtkSpeed"] = () => { weapon.data.dungeon_AddSpeed++; selectPanel.SetActive(false); };
        model.ButtonActions["Button_ShotCount"] = () => { weapon.data.dungeon_ShotCount++; selectPanel.SetActive(false); };





        // 4) UI ǥ��
        UIManager.Instance.ShowPanel(model);
    }

}
