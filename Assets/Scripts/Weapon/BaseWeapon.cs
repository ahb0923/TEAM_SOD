using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    public WeaponData data;  // ������ �������� : �ν����� â���� ����

    public string weaponId => data.id;
    public float Atk => data.attackPower;
    public float Speed => data.attackSpeed;
    //public float Delay => data.attackDelay;
    public float AttackRange => data.attackRange;

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
 