using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//무기 테스트용 스크립트

public class Test_WeaponAtk : MonoBehaviour
{
    public GameObject r;
    public GameObject m;
    public GameObject b;

   
    public RangeWeapon rangeWeapon;
    public RangeWeapon rangeWeapon1;
    public MeleeWeapon meleeWeapon;
    public Transform t; //테스트용 타겟
    private void Start()
    { 

        rangeWeapon = r.GetComponentInChildren<RangeWeapon>();
        rangeWeapon1 = b.GetComponentInChildren<RangeWeapon>();
        meleeWeapon = m.GetComponentInChildren<MeleeWeapon>();
    }
    void Update()
    {
        //플레이어에서 활 발사 로직 작성하실때
        //도움이 되길 바라며...
        if (rangeWeapon.data.attackRange >= Vector2.Distance(r.transform.position, t.position))
        {
            rangeWeapon.Attack(t.position);
        }
        else { rangeWeapon.animator.SetBool("IsAttack", false); }
        //활은 사거리 계산이 외부에 있어 애니메이션 종료를 외부에서 호출해야할 것 같아요...

        if (rangeWeapon1.data.attackRange >= Vector2.Distance(b.transform.position, t.position))
        {
            rangeWeapon1 .Attack(t.position);
        }
        else { rangeWeapon.animator.SetBool("IsAttack", false) ; }



        //반면 무기는 그냥 Update에서 호출하셔도 괜찮습니다.
        //메소드 내부에 사거리 계산을 하도록 하였습니다.


       





    }
}
