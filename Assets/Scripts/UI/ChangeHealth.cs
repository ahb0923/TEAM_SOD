using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealth : MonoBehaviour
{
    [Header("References")]
    [Tooltip("HP�� �����ϴ� StatController")]
    [SerializeField] private StatController statController;

    [Tooltip("HP �ٷ� ����� Slider")]
    [SerializeField] private Slider hpSlider;

    private void Awake()
    {
        // �� StatController �Ҵ� (�ν����Ϳ� �� �־�ξ��� �� �ڵ����� ���� GameObject���� ã����)
        if (statController == null)
            statController = GetComponent<StatController>();

        // �� Slider ������Ʈ �Ҵ� (�ν����Ϳ� �� �־�ξ��� ��)
        if (hpSlider == null)
            hpSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        // �� Slider�� �ִ밪�� StatController.MaxHp�� ����
        hpSlider.maxValue = statController.MaxHp;  // :contentReference[oaicite:0]{index=0}:contentReference[oaicite:1]{index=1}

        // �� �ʱⰪ�� ���� HP�� ����
        hpSlider.value = statController.Hp;        // :contentReference[oaicite:2]{index=2}:contentReference[oaicite:3]{index=3}
    }

    private void Update()
    {
        // �� �� ������ ���� HP�� Slider �� ����ȭ
        hpSlider.value = statController.Hp;        // :contentReference[oaicite:4]{index=4}:contentReference[oaicite:5]{index=5}
    }
}
