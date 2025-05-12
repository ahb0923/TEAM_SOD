using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test_WeaponAtk : MonoBehaviour
{
    public GameObject r;
    public GameObject m;

   
    public RangeWeapon rangeWeapon;
    public MeleeWeapon meleeWeapon;
    public Transform t;
    private void Start()
    { 

        rangeWeapon = r.GetComponentInChildren<RangeWeapon>();
        meleeWeapon = m.GetComponentInChildren<MeleeWeapon>();
    }
    void Update()
    {
        if (rangeWeapon.data.attackRange >= Vector2.Distance(r.transform.position, t.position))
        {
            rangeWeapon.Attack(t.position);
        }
        else { rangeWeapon.animator.SetBool("IsAttack", false); }
        

        meleeWeapon.Attack(t.position);





    }
}
