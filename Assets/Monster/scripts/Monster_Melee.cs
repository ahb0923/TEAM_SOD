using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Melee : Monster
{
    // Start is called before the first frame update
    void Start()
    {
        MaxHp = 10;

    }

    // Update is called once per frame
    void Update()
    {
       CheckPlayer();
    }
}
