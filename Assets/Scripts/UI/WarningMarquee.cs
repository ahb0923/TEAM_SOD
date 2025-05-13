using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMarquee : MonoBehaviour
{
    [Tooltip("스크롤 속도 (px/sec)")]
    [SerializeField] private float scrollSpeed = 200f;

    private RectTransform rt;
    private float textWidth;
    private float startX;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        // 초기 X 위치
        startX = rt.anchoredPosition.x;
        // 텍스트 전체 길이
        textWidth = rt.rect.width;
    }

    void Update()
    {
        // 1) 오른쪽에서 왼쪽으로 이동
        rt.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

        // 2) 왼쪽으로 완전히 지나가면 시작 위치로 복귀
        if (rt.anchoredPosition.x <= -textWidth)
        {
            rt.anchoredPosition = new Vector2(startX, rt.anchoredPosition.y);
        }
    }
}
