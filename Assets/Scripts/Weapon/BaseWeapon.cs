using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;  // ������ �������� : �ν����� â���� ����
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

    public float WeaponSize => data.weaponSize; //�������� ������
    public LayerMask target => data.layer; //Ÿ�� ���̾� ����


    public Animator animator;
    private SpriteRenderer weaponRenderer; 
    public AudioClip attackSoundClip; //�߻� ����� Ŭ��


    protected virtual void Awake() {}
    protected virtual void Start() {}

 
    public virtual void Attack(Vector3 v)
    {
        AttackAnimation(); //���� �ִϸ��̼�

        if (attackSoundClip) { } //���� Ŭ��
                                 // SoundManager.PlayClip(attackSoundClip);
    }

    public void AttackAnimation()
    {
        animator.SetFloat("AttackSpeed", Speed);
        animator.SetTrigger("IsAttack");
    }
}
 