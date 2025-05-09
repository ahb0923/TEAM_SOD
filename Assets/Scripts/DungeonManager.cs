using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [Header("���� Ŭ���� �� ��� UI �г�")]
    [SerializeField] private GameObject clearUIPanel;
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
        if (clearUIPanel != null)
            clearUIPanel.SetActive(true);
        else
            Debug.LogWarning("DungeonManager: clearUIPanel�� �Ҵ���� �ʾҽ��ϴ�!");
    }

}
