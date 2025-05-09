using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDoor : MonoBehaviour
{
    [SerializeField] private SCENE_TYPE targetScene;  // �ν����Ϳ��� Enum ����

    public void OnInteract()//BasePlayer player)
    {
        // Enum.ToString() ���� "DungeonScene" ���� �̸� �� ȣ��
        GameManager.Instance.ShowUI();
        SceneHandleManager.Instance.LoadScene(targetScene.ToString());
    }
}
