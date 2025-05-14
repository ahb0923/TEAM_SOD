using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMarquee : MonoBehaviour
{
    [Tooltip("��ũ�� �ӵ� (px/sec)")]
    [SerializeField] private float scrollSpeed = 200f;

    private RectTransform rt;
    private float textWidth;
    private float startX;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        // �ʱ� X ��ġ
        startX = rt.anchoredPosition.x;
        // �ؽ�Ʈ ��ü ����
        textWidth = rt.rect.width;
    }

    void Update()
    {
        // 1) �����ʿ��� �������� �̵�
        rt.anchoredPosition += Vector2.left * scrollSpeed * Time.deltaTime;

        // 2) �������� ������ �������� ���� ��ġ�� ����
        if (rt.anchoredPosition.x <= -textWidth)
        {
            rt.anchoredPosition = new Vector2(startX, rt.anchoredPosition.y);
        }
    }
}
