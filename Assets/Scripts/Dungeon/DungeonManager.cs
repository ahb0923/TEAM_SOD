using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [Header("던전 클리어 시 띄울 UI 패널")]
    [SerializeField] private GameObject clearUIPanel;


    [Header("플레이어 컨트롤러 (Inspector에 할당)")]
    [SerializeField] private MonoBehaviour playerController;
    private int remainingMonsters = 0;

   

    private void Awake()
    {
        
       

        // UI는 시작할 때 숨겨 놓기
        if (clearUIPanel != null)
            clearUIPanel.SetActive(false);
    }

    /// <summary>맵 타일이 몬스터를 하나 스폰할 때마다 호출</summary>
    public void RegisterMonster()
    {
        remainingMonsters++;
    }

    /// <summary>몬스터가 죽을 때마다 호출</summary>
    public void UnregisterMonster()
    {
        remainingMonsters--;
        if (remainingMonsters <= 0)
        {
            ShowClearUI();
        }
    }

    private void ShowClearUI()
    {
        
            clearUIPanel.SetActive(true);
        playerController.enabled = false;

    }

    public void Continue()
    {
        // 1) UI 숨기기
        clearUIPanel.SetActive(false);
        // 2) 플레이어 컨트롤 복원
        playerController.enabled = true;
        // 3) 다음 씬(또는 로비)으로 이동
        SceneHandleManager.Instance.LoadScene("LobbyScene");
    }

}
