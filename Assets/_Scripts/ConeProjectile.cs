using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class ConeProjectile : Projectile
{
    private Rigidbody2D _rigidbody;

    private GameObject playerobject;
    private PlayerInCombat player;
    private bool parryTrigger = false;
   


    private void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        direction = new Vector3(0, 0, 0);

        playerobject = GameObject.FindWithTag("Player");
        player = playerobject.GetComponent<PlayerInCombat>();
    }



    private void FixedUpdate()
    {
        
        Move();
        Debug.Log(speed);
    }

    protected override void Move()
    {

        if (!parryTrigger)
        {
            _rigidbody.velocity = speed * direction;
        }
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
            parryTrigger = true;
            direction = collision.transform.up;
            _rigidbody.velocity = new Vector2(0, 0);
            Invoke(nameof(Parry), 0.1f);
            maxWallBounces = 1;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (!player.invulnerability)
            {

                Vector3 dir = (direction).normalized;
                //StartCoroutine( player.Knockback(dir));
                player.KnockBack(dir);
               

                ProjectileDestruction();
            }
        }

        if (maxWallBounces <= 0)
        {
            ProjectileDestruction();
        }
    }

    private void ProjectileDestruction()
    {
        speed = 5;
        gameObject.SetActive(false);
        maxWallBounces = defaultMaxWallBounces;
    }

    private void Parry()
    {
        player.invulnerability = true;
        Invoke(nameof(player.Invulnerability), 0.5f);
        speed = speed * player.parryacceleration;
        parryTrigger = false;
    }

}
