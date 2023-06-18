using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ForwardProjectile : Projectile
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
            // Trigger screen shake when ball colliding with wall
            // For game feel
            CameraShaker.Instance.ShakeOnce(1f, 1.5f, .1f, .1f);
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
                player.KnockBack2(dir);
               

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
        gameObject.SetActive(false);
        speed = defaultSpeed;
        maxWallBounces = defaultMaxWallBounces;
    }
    private void Parry()
    {
        speed = speed * player.parryacceleration;
        parryTrigger = false;
    }
}
