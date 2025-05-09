using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data.Common;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI damageText;
    private Color damageColor;
    private Coroutine showAnimation;

    private Vector3 startPos;
    private Vector3 endPos;

    private float moveDistance = 20f;
    private float moveSpeed = 10f;
    private float duration = 1f;
    private float elapsed = 0f;

    private void Awake()
    {
        damageText = GetComponent<TextMeshProUGUI>();
        //초기화용 칼라값 저장
        damageColor = damageText.color;

        startPos = transform.localPosition;
        endPos = startPos + Vector3.up*moveDistance;
    }

    private void OnEnable()
    {
        if(showAnimation != null)
            StopCoroutine(showAnimation);

        showAnimation = StartCoroutine(TextMoving());
    }

    void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        damageText.color = damageColor;
        if (showAnimation != null)
        {
            StopCoroutine(showAnimation);
            showAnimation = null;
        }
    }

    public void SetDamage(int value)
    {
        damageText.text = value.ToString();
        transform.localPosition = Vector3.zero;
    }

    IEnumerator TextMoving()
    {
        yield return new WaitForSeconds(1f);

        while (elapsed < duration)
        {
            // 진행도 계산식입니다
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(startPos, endPos, t);

            // 위로 올라갈수록 투명해지도록 설정
            Color tempColor = damageText.color;
            damageText.color = new Color(tempColor.r, tempColor.g, tempColor.b, 1 - t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
