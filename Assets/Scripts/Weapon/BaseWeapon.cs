using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    private WeaponData data;  // ������ �������� : �ν����� â���� ����
    [SerializeField] private string weaponId;
    public float Delay => data.delay;
    public float WeaponSize => data.weaponSize;
    public float Power => data.AttackPower;
    public float Speed => data.AttackSpeed;
    public float AttackRange => data.AttackRange;


    private Animator animator;
    private SpriteRenderer weaponRenderer; 
    public AudioClip attackSoundClip; //�߻� ����� Ŭ��
    public LayerMask target; //Ÿ�� ���̾� ����

    private void Awake()
    {
        data = DataManager.Instance.GetWeaponData(weaponId);
        // data�� Ȱ���� ����

        //�ӽ� �ʱ�ȭ �ڵ�
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
 