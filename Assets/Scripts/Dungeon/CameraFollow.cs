using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("추적할 대상")]
    public Transform target;

    [Header("카메라 오프셋 (Z축은 보통 -10)")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("부드러운 추적 속도 (클수록 빠름)")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 = 플레이어 위치 + 오프셋
        Vector3 desiredPosition = target.position + offset;

        // 현재 카메라 위치와 목표 위치 사이를 lerp
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed
        );

        transform.position = smoothedPosition;
    }

}
