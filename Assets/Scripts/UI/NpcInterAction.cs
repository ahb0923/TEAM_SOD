using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class NpcInterAction : BaseInterAction
{
    [SerializeField] private GameObject eWard;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eWard.SetActive(true);
            _playerInRange = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            eWard.SetActive(false);
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

        model.ButtonActions["B"] = () =>
        {

        };

        UIManager.Instance.ShowPanel(model);

    }


}
