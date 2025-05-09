using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [Header("���� Ŭ���� �� ��� UI �г�")]
    [SerializeField] private GameObject clearUIPanel;


    [Header("�÷��̾� ��Ʈ�ѷ� (Inspector�� �Ҵ�)")]
    [SerializeField] private MonoBehaviour playerController;
    private int remainingMonsters = 0;

   

    private void Awake()
    {
        
       

        // UI�� ������ �� ���� ����
        if (clearUIPanel != null)
            clearUIPanel.SetActive(false);
    }

    /// <summary>�� Ÿ���� ���͸� �ϳ� ������ ������ ȣ��</summary>
    public void RegisterMonster()
    {
        remainingMonsters++;
    }

    /// <summary>���Ͱ� ���� ������ ȣ��</summary>
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
        // 1) UI �����
        clearUIPanel.SetActive(false);
        // 2) �÷��̾� ��Ʈ�� ����
        playerController.enabled = true;
        // 3) ���� ��(�Ǵ� �κ�)���� �̵�
        SceneHandleManager.Instance.LoadScene("LobbyScene");
    }

}
