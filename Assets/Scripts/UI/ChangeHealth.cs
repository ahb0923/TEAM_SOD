using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealth : MonoBehaviour
{
    [Header("References")]
    [Tooltip("HP를 관리하는 StatController")]
    [SerializeField] private StatController statController;

    [Tooltip("HP 바로 사용할 Slider")]
    [SerializeField] private Slider hpSlider;

    private void Awake()
    {
        // ① StatController 할당 (인스펙터에 안 넣어두었을 때 자동으로 같은 GameObject에서 찾아줌)
        if (statController == null)
            statController = GetComponent<StatController>();

        // ② Slider 컴포넌트 할당 (인스펙터에 안 넣어두었을 때)
        if (hpSlider == null)
            hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        // ③ Slider의 최대값을 StatController.MaxHp로 설정
        hpSlider.maxValue = statController.MaxHp;  // :contentReference[oaicite:0]{index=0}:contentReference[oaicite:1]{index=1}

        // ④ 초기값을 현재 HP로 설정
        hpSlider.value = statController.Hp;        // :contentReference[oaicite:2]{index=2}:contentReference[oaicite:3]{index=3}
    }

    private void Update()
    {
        // ⑤ 매 프레임 현재 HP로 Slider 값 동기화
        hpSlider.value = statController.Hp;        // :contentReference[oaicite:4]{index=4}:contentReference[oaicite:5]{index=5}
    }
}
