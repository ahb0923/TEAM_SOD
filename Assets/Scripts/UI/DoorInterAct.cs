using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInterAct : MonoBehaviour //, IInteractable
{
    [SerializeField] private string targetMapName;    // ¿¹: ¡°Dungeon01¡±
    public void OnInteract(BasePlayer player)
    {

        GameManager.Instance.LoadScene(targetMapName);
    }
}
