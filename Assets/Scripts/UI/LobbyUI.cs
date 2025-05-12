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
        //// GameManager.Instance.Player가 null이 아니어야 합니다.
        //player = GameManager.Instance.Player;
        
    }

    void Update()
    {
        //if (player == null) return;

        //if (player.Money != _lastMoney) //플레이어가 가지고 있는 머니가 바뀌었을 때 // 플레이어 머니 추가 
        //{
        //    _lastMoney = player.Money;
        //    moneyText.text = $"{_lastMoney}";
        //}
    }
}
