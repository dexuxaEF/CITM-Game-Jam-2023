using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthEnemy : Enemy
{
    [Range(1.0f,180f)]
    public float coneAngle = 5f;

    [Range(2,6)]
    public int numProjectiles = 2;

    [Tooltip("defaultAttack: only 2 projectiles")]
    public bool defaultAttack = true;


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


    }

    public override void Attack()
    {
        base.Attack();

        //if (Input.GetMouseButtonDown(0))
        //{
        //    SpawnLeftProjectile();
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    SpawnRightProjectile();
        //}



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
            projectileBehavior.direction = Quaternion.Euler(0f, 0f, -coneAngle) * playerDirection;

        }
        projectilePool.Enqueue(projectile);
    }

    public void SpawnMultipleProjectile()
    {
        //aux to get the cone projectiles centered to the playerDirection
        int aux = Mathf.RoundToInt(numProjectiles / 2.0f);

        for (int i=0-aux; i<numProjectiles-aux; i++)
        {
            
            GameObject projectile = projectilePool.Dequeue();
            if (projectile != null)
            {
                projectile.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
                ConeProjectile projectileBehavior = projectile.GetComponent<ConeProjectile>();
                projectile.SetActive(true);
                projectileBehavior.direction = Quaternion.Euler(0f, 0f, coneAngle*i) * playerDirection;

            }
            projectilePool.Enqueue(projectile);
        }
       
    }


    private void CoroutineWithDelay()
    {
        StartCoroutine(ProjectileCoroutine());
        
    }

    IEnumerator ProjectileCoroutine()
    {
        while (true)
        {
            if(defaultAttack)
            {
                SpawnLeftProjectile();
                SpawnRightProjectile();
            }
            else
            {
                SpawnMultipleProjectile();
            }
            
            yield return new WaitForSeconds(Random.Range(minReloadTime, maxReloadTime));
        }
    }



}
