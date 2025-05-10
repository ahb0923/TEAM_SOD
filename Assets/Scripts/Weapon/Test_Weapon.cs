using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Weapon : MonoBehaviour
{
    
    public MeleeWeapon m;
    public Transform t;
    void Update()
    {
        if (Vector2.Distance(m.transform.position, t.position) <= m.data.attackRange)
        {
           // m.Attack(t.position);
        }
        
    }
}
