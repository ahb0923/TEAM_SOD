using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitionDoor : MonoBehaviour
{
    [Header("�÷��̾ �̵��� ��ǥ ����")]
    [SerializeField] private Transform playerTarget;
    [Header("ī�޶� �̵��� ��ǥ ����")]
    [SerializeField] private Transform cameraTarget;
    [Header("�̵� �ð�(��)")]
    [SerializeField] private float transitionDuration = 1f;

    bool _playerInRange;
    Transform _playerTransform;
    MonoBehaviour _playerController; // (��: Player_Dungeon)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �÷��̾� �±׷� �Ǻ�
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            _playerTransform = other.transform;
            // �÷��̾� ���� ��ũ��Ʈ ��Ȱ��ȭ�� ���� ���� ����
           // _playerController = other.GetComponent<Player_Dungeon>()
                             // ?? (MonoBehaviour)other.GetComponent<BasePlayer>();
            // (����) ��Press E�� UI ǥ��
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            _playerTransform = null;
            _playerController = null;
            // (����) ��Press E�� UI �����
        }
    }

    private void Update()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        // 1) ��� ���� ��ױ�
        if (_playerController != null)
            _playerController.enabled = false;

        // 2) ����/�� ��ġ ����
        Vector3 camStart = Camera.main.transform.position;
        Vector3 camEnd = new Vector3(cameraTarget.position.x, cameraTarget.position.y, camStart.z);
        Vector3 plyStart = _playerTransform.position;
        Vector3 plyEnd = playerTarget.position;

        float t = 0f;
        while (t < transitionDuration)
        {
            t += Time.deltaTime;
            float f = Mathf.SmoothStep(0f, 1f, t / transitionDuration);
            Camera.main.transform.position = Vector3.Lerp(camStart, camEnd, f);
            _playerTransform.position = Vector3.Lerp(plyStart, plyEnd, f);
            yield return null;
        }

        // 3) ���� ����
        Camera.main.transform.position = camEnd;
        _playerTransform.position = plyEnd;

        // 4) ���� ����
        if (_playerController != null)
            _playerController.enabled = true;
    }

    // (����) �� �信�� ��ġ Ȯ�ο�
    private void OnDrawGizmosSelected()
    {
        if (playerTarget)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(playerTarget.position, 0.3f);
        }
        if (cameraTarget)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(cameraTarget.position, Vector3.one * 0.5f);
        }
    }
}
