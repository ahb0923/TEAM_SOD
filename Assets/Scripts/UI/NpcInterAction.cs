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

        model.ButtonActions["Button_BasicBow"] = () =>
        {
            StartCoroutine(SetPlayerWeapon(model, "BasicBowData_p"));
        };

        model.ButtonActions["Button_BlueBow"] = () =>
        {
            StartCoroutine(SetPlayerWeapon(model, "BlueBowData_p"));
        };

        model.ButtonActions["Button_RedBow"] = () =>
        {
            StartCoroutine(SetPlayerWeapon(model, "RedBowData_p"));
        };



        model.ButtonActions["Button_Cancel"] = () =>
        {
            //buttonAnimator_blue.SetTrigger("Press_blue");

            StartCoroutine(CancelPanel(model));

        };






        UIManager.Instance.ShowPanel(model);

    }

    private IEnumerator SetPlayerWeapon(PanelModel model,string WeaponDataID)
    {
        //buttonAnimator.SetTrigger("Press"); //추후 버튼애니메이션 할당
        Debug.Log("버튼 눌림");
        BasePlayer player = FindFirstObjectByType<BasePlayer>();
        if (player == null) Debug.Log("플레이어 못 불러옴");
        else Debug.Log("BasePlayer 컴포잘 불러옴");
        DataManager.Instance.WriteWeaponDataMap(); // 이거 얘기해봐야 함(매번 새로 불러옴)
        player.PlayerWeaponSelect(DataManager.Instance.GetWeaponData(WeaponDataID));
        StartCoroutine(CancelPanel(model));
        yield return null;
    }

    private IEnumerator CancelPanel(PanelModel model)
    {
        yield return new WaitForSeconds(0.2f);
        UIManager.Instance.ClosePanel(model);
    }
}
