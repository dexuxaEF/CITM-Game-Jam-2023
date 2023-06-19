using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;

[RequireComponent(typeof(Rigidbody2D))]
public class ConeProjectile : Projectile
{
    private Rigidbody2D _rigidbody;

    private GameObject playerobject;
    private PlayerInCombat player;

    private GameObject heartobject;
    private HearthEnemy heart;
    private bool parryTrigger = false;
    public bool isparried = false;



    private void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        direction = new Vector3(0, 0, 0);

        playerobject = GameObject.FindWithTag("Player");
        player = playerobject.GetComponent<PlayerInCombat>();

        heartobject = GameObject.FindWithTag("Heart");
        heart = heartobject.GetComponent<HearthEnemy>();
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
            CameraShaker.Instance.ShakeOnce(1f, 1.5f, .1f, .1f);
        }

        if (collision.gameObject.CompareTag("Parry"))
        {
            parryTrigger = true;
            isparried = true;
            player.invulnerability = true;
            //direction = collision.transform.up;
            Vector2 worldPos = Input.mousePosition;
            worldPos = Camera.main.ScreenToWorldPoint(worldPos);
            direction = (worldPos - _rigidbody.position).normalized;
            _rigidbody.velocity = new Vector2(0, 0);
            Invoke(nameof(Parry), 0.2f);
            maxWallBounces = 1;
            return;
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

      

        if (collision.gameObject.CompareTag("Heart") && isparried)
        {

            if (!heart.invulnerability)
            {

                heart.lives--;
                ProjectileDestruction();
            }

        }

        

        if (maxWallBounces <= 0)
        {
            ProjectileDestruction();
        }
    }

    public void ProjectileDestruction()
    {
        speed = defaultSpeed;
        gameObject.SetActive(false);
        maxWallBounces = defaultMaxWallBounces;
    }

    private void Parry()
    {
        player.invulnerabilityParry = true;
        speed = speed * player.parryacceleration;
        parryTrigger = false;
    }

}
