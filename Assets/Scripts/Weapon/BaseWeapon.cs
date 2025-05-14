using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;  // 데이터 가져오기 : 인스펙터 창에서 연결
    public RewardData rewardData;
    public string id => data.dataKey;
    public float Atk { 
        get
        {
            if(rewardData == null)
            {
                return data.attackPower;
            }
            else
            {
                Debug.Log(data.attackPower + rewardData.weaponPower);
                return data.attackPower+rewardData.weaponPower;
            }

        }
         
    }
    public float Speed
    {
        get
        {
            if (rewardData == null)
            {
                return data.attackSpeed;
            }
            else
            {
                return data.attackSpeed + rewardData.weaponSpeed;
            }

        }
    }
    //public float Delay => data.attackDelay;
    public float AttackRange
    {
        get
        {
            if (rewardData == null)
            {
                return data.attackSpeed;
            }
            else
            {
                return data.attackSpeed + rewardData.weaponSpeed;
            }

        }
    }

    public float WeaponSize => data.weaponSize; //근접무기 사이즈
    public LayerMask target => data.layer; //타겟 레이어 지정


    public Animator animator;
    private SpriteRenderer weaponRenderer; 
    public AudioClip attackSoundClip; //발사 오디오 클립


    protected virtual void Awake() {}
    protected virtual void Start() {}

 
    public virtual void Attack(Vector3 v)
    {
        AttackAnimation(); //공격 애니메이션

        if (attackSoundClip) { } //사운드 클립
                                 // SoundManager.PlayClip(attackSoundClip);
    }

    public void AttackAnimation()
    {
        animator.SetFloat("AttackSpeed", Speed);
        animator.SetTrigger("IsAttack");
    }
}
 