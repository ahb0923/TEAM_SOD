using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("������ ���")]
    public Transform target;

    [Header("ī�޶� ������ (Z���� ���� -10)")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("�ε巯�� ���� �ӵ� (Ŭ���� ����)")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (target == null) return;

        // ��ǥ ��ġ = �÷��̾� ��ġ + ������
        Vector3 desiredPosition = target.position + offset;

        // ���� ī�޶� ��ġ�� ��ǥ ��ġ ���̸� lerp
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed
        );

        transform.position = smoothedPosition;
    }

}
