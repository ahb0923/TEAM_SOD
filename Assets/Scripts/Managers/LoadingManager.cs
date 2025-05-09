using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    public Slider progressBar;

    private void Start()
    {
        StartCoroutine(LoadTargetScene());
    }

    IEnumerator LoadTargetScene()
    {
        string sceneName = SceneHandleManager.Instance.NextSceneName;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        // 로딩 진행중
        while(operation.progress < 0.9f)
        {
            if (progressBar != null)
                progressBar.value = operation.progress;

            yield return null;
        }

        // 로딩 완료
        if (progressBar != null)
            progressBar.value = 1f;

        // 1초 기다렸다가 화면 전환
        yield return new WaitForSeconds(1f); 

        operation.allowSceneActivation = true;
    }
}
