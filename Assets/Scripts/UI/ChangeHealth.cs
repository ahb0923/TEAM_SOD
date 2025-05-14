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
        // StatController 할당 

        statController =player.GetComponent<StatController>();

        // Slider 컴포넌트 할당 
        
            hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        // Slider의 최대값을 StatController.MaxHp로 설정
        hpSlider.maxValue = statController.MaxHp;  // :contentReference[oaicite:0]{index=0}:contentReference[oaicite:1]{index=1}

        // 초기값을 현재 HP로 설정
        hpSlider.value = statController.MaxHp;        // :contentReference[oaicite:2]{index=2}:contentReference[oaicite:3]{index=3}
    }

    private void Update()
    {
        // 매 프레임 현재 HP로 Slider 값 동기화
        hpSlider.value = statController.Hp;        // :contentReference[oaicite:4]{index=4}:contentReference[oaicite:5]{index=5}
    }
}
