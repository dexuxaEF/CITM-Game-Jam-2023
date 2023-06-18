using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectile : Projectile
{
    private Rigidbody2D _rigidbody;

    private void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        direction = new Vector3(0, 0, 0);
    }



    private void FixedUpdate()
    {

        Move();
    }

    protected override void Move()
    {

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
            ProjectileDestruction();
        }

        if (maxWallBounces <= 0)
        {
            ProjectileDestruction();
        }
    }

    private void ProjectileDestruction()
    {
        gameObject.SetActive(false);
        maxWallBounces = defaultMaxWallBounces;
    }
}
