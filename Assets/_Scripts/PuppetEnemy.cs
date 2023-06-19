using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetEnemy : Enemy
{
    public bool isCurved = true;

    [Tooltip("notDefaultAttack: kinda tracking projectile")]
    public bool defaultAttack = true;

    private void Awake()
    {
        
    }
    void Start()
    {
        base.Start();

        projectileParent = new GameObject("PuppetProjectileParent");

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

        if(isMoving && isCurved) 
        {
            this.transform.position = new Vector3(this.transform.position.x + Mathf.Sin((0.8f * Time.deltaTime)),
                                                this.transform.position.y + Mathf.Sin((0.8f * Time.deltaTime)),
                                                this.transform.position.z);
        }
    }

    public override void Attack()
    {
        base.Attack();
    }

    public void SpawnProjectile()
    {
        GameObject projectile = projectilePool.Dequeue();
        if (projectile != null)
        {
            projectile.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
            ForwardProjectile projectileBehavior = projectile.GetComponent<ForwardProjectile>();
            projectile.SetActive(true);
            projectileBehavior.direction = playerDirection;
           

            if (defaultAttack)
            {
                projectileBehavior.isTracking = false;
            }
            else
            {
                projectileBehavior.isTracking = true;
            }
            

        }
        projectilePool.Enqueue(projectile);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void CoroutineWithDelay()
    {
        StartCoroutine(ProjectileCoroutine());

    }

    IEnumerator ProjectileCoroutine()
    {
        while (true)
        {
            SpawnProjectile();
            yield return new WaitForSeconds(Random.Range(minReloadTime, maxReloadTime));
        }
    }
}
