using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private WeaponData data;  // ������ �������� : �ν����� â���� ����

    public string weaponId => data.id;
    public float Atk => data.attackPower;
    public float Speed => data.attackSpeed;
    public float Delay => data.attackDelay;
    public float AttackRange => data.attackRange;


    private Animator animator;
    private SpriteRenderer weaponRenderer; 
    public AudioClip attackSoundClip; //�߻� ����� Ŭ��
    public LayerMask target; //Ÿ�� ���̾� ����

    private void Awake()
    {
       
    }
    protected virtual void Start()
    {

    }

    public virtual void Attack()
    {
        AttackAnimation(); //���� �ִϸ��̼�

        if (attackSoundClip) { } //���� Ŭ��
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
 