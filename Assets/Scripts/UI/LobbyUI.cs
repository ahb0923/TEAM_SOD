using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private BasePlayer player;
    private int _lastMoney = -1;
    private void Awake()
    {
        //// GameManager.Instance.Player�� null�� �ƴϾ�� �մϴ�.
        //player = GameManager.Instance.Player;
        
    }

    void Update()
    {
        //if (player == null) return;

        //if (player.Money != _lastMoney) //�÷��̾ ������ �ִ� �Ӵϰ� �ٲ���� �� // �÷��̾� �Ӵ� �߰� 
        //{
        //    _lastMoney = player.Money;
        //    moneyText.text = $"{_lastMoney}";
        //}
    }
}
