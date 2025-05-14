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

    [SerializeField] private GameObject eWard;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //eWard.SetActive(true);
            _playerInRange = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //eWard.SetActive(false);
            _playerInRange = false;
        }
    }





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
            buttonAnimator.SetTrigger("Press");

            StartCoroutine(EnterDungeon());

        };
       
        model.ButtonActions["Button_Cancel"] = () =>
        {
            buttonAnimator_blue.SetTrigger("Press_blue");

            StartCoroutine(CancelPanel(model));

        };

        UIManager.Instance.ShowPanel(model);
    }
    private IEnumerator EnterDungeon()
    {
        // 애니메이션이 재생되는 시간만큼 대기
        yield return new WaitForSeconds(0.2f);
        SceneHandleManager.Instance.LoadScene(SCENE_TYPE.DungeonScene.ToString());
    }

    private IEnumerator CancelPanel(PanelModel model)
    {
        yield return new WaitForSeconds(0.2f);
        UIManager.Instance.ClosePanel(model);
    }


}
