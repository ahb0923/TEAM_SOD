using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    private Transform pivot;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = direction * speed;
    }

    public void Init(Vector2 direction)
    {
        this.direction = direction;
        if(this.direction.x < 0)
        {
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        }
        else {
            pivot.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
