using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActionZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered the Zone");
            SceneHandleManager.Instance.LoadScene(SCENE_TYPE.DungeonScene.ToString());
        }
    }
}
