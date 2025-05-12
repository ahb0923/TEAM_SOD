using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


 public class Monster: MonoBehaviour
 {
    public enum Monster_Type { Normal, Boss }
    private Monster_Type type;
    public Monster_Type Type { get { return Type; } }

    public GameObject target;
    private float checkRange;

    public event Action OnDeath;
    public float CheckRange { get { return checkRange; } set => checkRange = value; }

    private float attackRange;
    public float AttackRange { get { return attackRange; } set => attackRange = value; }

    Rigidbody2D rigidbody;  


    private void Awake()
    {
        
    }
    private void Update()
    {
    }

    private void FixedUpdate()
    {
    }
    public virtual void Attack() { }
   
    
    public virtual void Move(GameObject player)
    {
    }

    public virtual void CheckPlayer()
    {
        target = GameObject.FindWithTag("Player");
        float distance = Mathf.Abs(Vector2.Distance(target.transform.position, transform.position));
        if (distance <= CheckRange)
        {
            Move(target);
        }
    }

    public virtual void Death()
    {
        // 2) ������ ȣ��
        OnDeath?.Invoke();

        // 3) ���� Unregister (�ʿ��ϴٸ� �� ���� ����� ������ ����ص� �˴ϴ�)
        DungeonManager.Instance.UnregisterMonster();

        Destroy(gameObject);
    }

}
