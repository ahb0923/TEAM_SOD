using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private WeaponData data;  // 데이터 가져오기 : 인스펙터 창에서 연결
    [SerializeField] private string weaponId;
    public float Delay => data.delay;
    public float WeaponSize => data.weaponSize;
    public float Power => data.AttackPower;
    public float Speed => data.AttackSpeed;
    public float AttackRange => data.AttackRange;


    private Animator animator;
    private SpriteRenderer weaponRenderer; 
    public AudioClip attackSoundClip; //발사 오디오 클립
    public LayerMask target; //타겟 레이어 지정

    private void Awake()
    {
        data = DataManager.Instance.GetWeaponData(weaponId);
        // data를 활용해 세팅

        //임시 초기화 코드
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();
        animator.speed = 1f / Delay;
        transform.localScale = Vector3.one * WeaponSize;
    }
    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation();

        if (attackSoundClip) { }
           // SoundManager.PlayClip(attackSoundClip);
    }

    public void AttackAnimation()
    {
        //animator.SetTrigger(IsAttack);
    }
    public virtual void Rotate(bool isLeft)
    {
       // weaponRenderer.flipY = isLeft;
    }

}
 