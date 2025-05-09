using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬 전환 시 하드코딩 방지용 enumerator
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

    // 매니저들을 관리하는 최상위 "Manager" 오브젝트에 attach
    // 현재로서는 파괴방지 일괄 관리 그외 역할 없음(추후 추가 가능성 있음)
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
