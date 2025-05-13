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
    public GameObject c;
    public GameObject d;

   
    public RangeWeapon rangeWeapon;
    public RangeWeapon rangeWeapon1;
    public RangeWeapon rangeWeapon2;
    public RangeWeapon rangeWeapon3;
    public MeleeWeapon meleeWeapon;
    public Transform t; //테스트용 타겟
    private void Start()
    { 

        rangeWeapon = r.GetComponentInChildren<RangeWeapon>();
        rangeWeapon1 = b.GetComponentInChildren<RangeWeapon>();
        rangeWeapon2 = c.GetComponentInChildren<RangeWeapon>();
        rangeWeapon3 = d.GetComponentInChildren<RangeWeapon>();

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

        //활은 사거리 계산이 외부에 있어 애니메이션 종료를 외부에서 호출해야할 것 같아요...

        //지팡이~!
        if (rangeWeapon1.data.attackRange >= Vector2.Distance(b.transform.position, t.position))
        {
            rangeWeapon1.Attack(t.position);
        }

        if (rangeWeapon2.data.attackRange >= Vector2.Distance(c.transform.position, t.position))
        {
            rangeWeapon2.Attack(t.position);
        }

        if (rangeWeapon3.data.attackRange >= Vector2.Distance(d.transform.position, t.position))
        {
            rangeWeapon3.Attack(t.position);
        }


        //반면 무기는 그냥 장착하셔도 공격이 자동으로 됩니다.
        //메소드 내부에 사거리 계산을 하도록 하였습니다.








    }
}
