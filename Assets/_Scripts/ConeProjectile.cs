using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class ConeProjectile : Projectile
{
    private Rigidbody2D _rigidbody;

    private Action<ConeProjectile> OnKill;


    private void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        direction = new Vector3(0, 0, 0);
    }

    public void Init(Action<ConeProjectile> actionKill)
    {
        OnKill = actionKill;
    }


    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        Debug.Log(direction);
        _rigidbody.velocity = speed * direction;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            maxWallBounces -= 1;
            Vector2 wallNormal = collision.transform.up;
            direction = Vector2.Reflect(direction, wallNormal).normalized;
        }

        if (collision.gameObject.CompareTag("Parry"))
        {
            direction = collision.transform.up;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            OnKill(this);
        }

        if (maxWallBounces <= 0)
        {
            OnKill(this);
        }
    }


}
