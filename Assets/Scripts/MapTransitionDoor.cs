using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransitionDoor : MonoBehaviour
{
    [Header("플레이어가 이동할 목표 지점")]
    [SerializeField] private Transform playerTarget;
    [Header("카메라가 이동할 목표 지점")]
    [SerializeField] private Transform cameraTarget;
    [Header("이동 시간(초)")]
    [SerializeField] private float transitionDuration = 1f;

    bool _playerInRange;
    Transform _playerTransform;
    MonoBehaviour _playerController; // (예: Player_Dungeon)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 태그로 판별
        if (other.CompareTag("Player"))
        {
            _playerInRange = true;
            _playerTransform = other.transform;
            // 플레이어 제어 스크립트 비활성화를 위해 참조 저장
           // _playerController = other.GetComponent<Player_Dungeon>()
                             // ?? (MonoBehaviour)other.GetComponent<BasePlayer>();
            // (선택) “Press E” UI 표시
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInRange = false;
            _playerTransform = null;
            _playerController = null;
            // (선택) “Press E” UI 숨기기
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
        // 1) 잠시 제어 잠그기
        if (_playerController != null)
            _playerController.enabled = false;

        // 2) 시작/끝 위치 저장
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

        // 3) 최종 보정
        Camera.main.transform.position = camEnd;
        _playerTransform.position = plyEnd;

        // 4) 제어 복원
        if (_playerController != null)
            _playerController.enabled = true;
    }

    // (선택) 씬 뷰에서 위치 확인용
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
