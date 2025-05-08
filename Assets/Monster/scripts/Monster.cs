using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


abstract class Monster: MonoBehaviour
{
    public enum Monster_Type { Normal, Boss }
    private Monster_Type type;
    public Monster_Type Type { get { return Type; } }

    private float maxHp;
    public float MaxHp { get { return maxHp; } }

    private float hp;
    public float Hp { get { return hp; } }

    private float atk;
    public float Atk { get { return atk; } }

    private float def;
    public float Def { get { return def; } }

    private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    private float checkRange;
    public float CheckRange { get { return checkRange; } }

    Rigidbody2D rigidbody;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckPlayer();
    }

    private void FixedUpdate()
    {
    }
    public abstract void Attack();
    
    public void Move(GameObject player)
    {
        Vector2 direction = (transform.position - player.transform.position);
        rigidbody.velocity = direction * moveSpeed * Time.deltaTime;

    }

    public void CheckPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        float distance = Mathf.Abs(Vector2.Distance(player.transform.position, transform.position));
        if (distance <= CheckRange) 
        {
            Move(player);
        }
        
    }

}
