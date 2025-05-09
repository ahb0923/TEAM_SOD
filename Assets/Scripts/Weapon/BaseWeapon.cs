using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private WeaponData data;  // 데이터 가져오기 : 인스펙터 창에서 연결

    public string weaponId => data.id;
    public float Atk => data.attackPower;
    public float Speed => data.attackSpeed;
    public float Delay => data.attackDelay;
    public float AttackRange => data.attackRange;


    private Animator animator;
    private SpriteRenderer weaponRenderer; 
    public AudioClip attackSoundClip; //발사 오디오 클립
    public LayerMask target; //타겟 레이어 지정

    private void Awake()
    {
       
    }
    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation(); //공격 애니메이션

        if (attackSoundClip) { } //사운드 클립
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
 