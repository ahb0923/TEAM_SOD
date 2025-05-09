using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ȯ �� �ϵ��ڵ� ������ enumerator
enum MANAGER_TYPE{
    GameManager,
    UIManager,
    SceneManager,
    DungeonManager,
    AudioManager
}
public class Manager : MonoBehaviour
{
    private static bool checkInit = false;

    // �Ŵ������� �����ϴ� �ֻ��� "Manager" ������Ʈ�� attach
    // ����μ��� �ı����� �ϰ� ���� �׿� ���� ����(���� �߰� ���ɼ� ����)
    private void Awake()
    {
        if (checkInit)
        {
            Destroy(gameObject);
            return;
        }

        checkInit = true;
        DontDestroyOnLoad(gameObject);
    }
}
