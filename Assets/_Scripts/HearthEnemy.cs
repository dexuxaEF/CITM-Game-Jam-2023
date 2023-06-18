using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthEnemy : Enemy
{
    public float coneAngle = 30f;

    private void Start()
    {
        base.Start();

        projectileParent = new GameObject("HeartProjectileParent");

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileParent.transform);
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);
        }

        playerDirection = (player.transform.position - this.transform.position);
        Invoke(nameof(CoroutineWithDelay), delayTimeToAttack);


    }
    

    void Update()
    {
        if (health <= 0) 
            Die();

        playerDirection = (player.transform.position - this.transform.position).normalized;

        Move();
        Attack();
    }

    public override void Move()
    {
        base.Move();

        transform.Translate(playerDirection * speed*Time.deltaTime);
    }

    public override void Attack()
    {
        base.Attack();

        if (Input.GetMouseButtonDown(0))
        {
            SpawnLeftProjectile();
        }

        if (Input.GetMouseButtonDown(1))
        {
            SpawnRightProjectile();
        }



    }

    public void SpawnLeftProjectile()
    {


        GameObject projectile = projectilePool.Dequeue();
        if (projectile != null)
        {
            projectile.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            ConeProjectile projectileBehavior = projectile.GetComponent<ConeProjectile>();
            projectile.SetActive(true);
            projectileBehavior.direction = Quaternion.Euler(0f, 0f, coneAngle) * playerDirection;            

        }
            projectilePool.Enqueue(projectile);

        

    }

    public void SpawnRightProjectile()
    {
        GameObject projectile = projectilePool.Dequeue();
        if (projectile != null)
        {
            projectile.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            ConeProjectile projectileBehavior = projectile.GetComponent<ConeProjectile>();
            projectile.SetActive(true);
            projectileBehavior.direction = Quaternion.Euler(0f, 0f, -coneAngle) * playerDirection.normalized;

        }
        projectilePool.Enqueue(projectile);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Trigger con el Proyectil del Player
        if (collision.gameObject.CompareTag("Player"))
        {
            health -= getDamage;
        }
            

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enemy");
    }


    private void CoroutineWithDelay()
    {
        StartCoroutine(ProjectileCoroutine());
        
    }

    IEnumerator ProjectileCoroutine()
    {
        while (true)
        {
            SpawnLeftProjectile();
            SpawnRightProjectile();
            yield return new WaitForSeconds(reloadTime); // Espera 1 segundo
        }
    }


}
