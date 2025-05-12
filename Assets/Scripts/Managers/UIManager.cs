using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class PanelModel
{
    public GameObject Panel;
    public Dictionary<string, Sprite> Sprites;
    public Dictionary<string, TextMeshProUGUI> TextPro;
    public Dictionary<string, UnityAction> ButtonActions;

    public PanelModel()
    {
        Sprites = new Dictionary<string, Sprite>();
        ButtonActions = new Dictionary<string, UnityAction>();
        TextPro = new Dictionary<string, TextMeshProUGUI>();
    }
}





public class UIManager : Singleton<UIManager>
{





    public void ShowPanel(PanelModel model)
    {
        if (model.Panel == null) return;


        // 1) 패널 활성화
        model.Panel.SetActive(true);



        // 4) Sprites 딕셔너리 적용
        foreach (var kv in model.Sprites)
        {

            var imgTrans = model.Panel.transform.Find(kv.Key);
            if (imgTrans == null) continue;

            var img = imgTrans.GetComponent<Image>();
            if (img != null)
                img.sprite = kv.Value;
        }

        // 3) 텍스트 ("MessageText" 이름 가진 TMP_Text)
        foreach (var kv in model.TextPro)
        {

            var textTrans = model.Panel.transform.Find(kv.Key);
            if (textTrans == null) continue;

            var text = textTrans.GetComponent<TextMeshProUGUI>();
            if (text != null)
                text.text = kv.Value.text;
        }

        // 4) 버튼 콜백 설정
        foreach (var kv in model.ButtonActions)
        {
            var btnTrans = model.Panel.transform.Find(kv.Key);
            if (btnTrans == null) continue;

            var button = btnTrans.GetComponent<Button>();
            if (button == null) continue;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(kv.Value);
        }
    }




    //public bool IsShowUIActive(PanelModel panelModel)
    //{
    //    // 만약에 플레이어 생기면 플레이어 제어하는 함수에 
    //    //if (UIManager.Instance.IsShowUIActive) 
    //    //{ 
    //    //  Rigidbody 변수.velocity = Vector2.zero;
    //    //  return
    //    //}
    //    return panelModel.Panel.activeSelf;
    //}

    public void ClosePanel(PanelModel model)
    {
        model.Panel.SetActive(false);



    }






    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
