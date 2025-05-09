using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public string id;
    public float moveSpeed;
    public Sprite icon;
}
