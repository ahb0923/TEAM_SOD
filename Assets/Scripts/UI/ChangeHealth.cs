using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealth : MonoBehaviour
{

    private GameObject player;
    private StatController statController;

    [Tooltip("Slider")]
    [SerializeField] private Slider hpSlider;

    private void Awake()
    {
        player =GameObject.FindWithTag("Player");
        // StatController �Ҵ� 

        statController =player.GetComponent<StatController>();

        // Slider ������Ʈ �Ҵ� 
        
            hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        // Slider�� �ִ밪�� StatController.MaxHp�� ����
        hpSlider.maxValue = statController.MaxHp;  // :contentReference[oaicite:0]{index=0}:contentReference[oaicite:1]{index=1}

        // �ʱⰪ�� ���� HP�� ����
        hpSlider.value = statController.MaxHp;        // :contentReference[oaicite:2]{index=2}:contentReference[oaicite:3]{index=3}
    }

    private void Update()
    {
        // �� ������ ���� HP�� Slider �� ����ȭ
        hpSlider.value = statController.Hp;        // :contentReference[oaicite:4]{index=4}:contentReference[oaicite:5]{index=5}
    }
}
