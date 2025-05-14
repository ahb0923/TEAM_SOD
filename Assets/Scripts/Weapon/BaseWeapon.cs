using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;  // ������ �������� : �ν����� â���� ����
    public RewardData RewardData { get; set; }

    public string id => data.dataKey;
    public float Atk => data.attackPower + p_weaponPower;
    public float Speed => data.attackSpeed + p_weaponSpeed;
    //public float Delay => data.attackDelay;
    public float AttackRange => data.attackRange + p_weaponRange;

    public float WeaponSize => data.weaponSize; //�������� ������
    public LayerMask target => data.layer; //Ÿ�� ���̾� ����


    public Animator animator;
    private SpriteRenderer weaponRenderer; 
    public AudioClip attackSoundClip; //�߻� ����� Ŭ��

    // [����]���� ��� ����
    protected float p_weaponPower;
    protected float p_weaponSpeed;
    protected float p_weaponRange;

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

    public virtual void SettingStat()
    {

    }

}
 