using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class HearthEnemy : Enemy
{
    public int damage = 3;

    [Header("Object Pool")]
    public IObjectPool<ConeProjectile> pool;
    public bool collectionCheks = false;
    public int defaultCapacity = 15;
    public int maxCapacity = 25;

    public Vector3 playerDirection;
    
    public ConeProjectile projectilePrefab;

    private void Start()
    {
        base.Start();

        pool = new ObjectPool<ConeProjectile>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyPoolObject,
            collectionCheks,
            defaultCapacity,
            maxCapacity
            );

    }
    

    void Update()
    {
        if (health <= 0) 
            Die();

        Move();
        Attack();
    }

    public override void Move()
    {
        base.Move();

        Vector3 direction = (player.transform.position - this.transform.position).normalized;

        transform.Translate(direction*speed*Time.deltaTime);
    }

    public override void Attack()
    {
        base.Attack();

        playerDirection = (player.transform.position - this.transform.position);


        if (Input.GetMouseButtonDown(0))
        {
            SpawnLeftProjectile();
        }

        //Invoke(nameof(SpawnLeftProjectile), 1.0f);
        //Invoke(nameof(SpawnRightProjectile), 3.3f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Enemy");
    }


    public void SpawnLeftProjectile()
    {
        var projectile = pool.Get();
        projectile.transform.position = this.transform.position;
        projectile.direction = playerDirection.normalized;
        projectile.Init(Kill);
    }

    public void SpawnRightProjectile()
    {
        var projectile = pool.Get();
        projectile.transform.position = this.transform.position;
        projectile.direction = playerDirection.normalized;
        projectile.Init(Kill);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Trigger con el Proyectil del Player
        if (collision.gameObject.CompareTag("Player"))
        {
            health -= damage;
        }
            

    }

    #region ObjectPool
    private ConeProjectile CreatePooledItem()
    {
        return Instantiate(projectilePrefab);
    }


    // Called when an item is returned to the pool using Release
    private void OnReturnedToPool(ConeProjectile system)
    {
        system.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    private void OnTakeFromPool(ConeProjectile system)
    {
        system.gameObject.SetActive(true);

    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    private void OnDestroyPoolObject(ConeProjectile system)
    {
        Destroy(system.gameObject);
    }

    private void Kill(ConeProjectile projectile)
    {
        pool.Release(projectile);
    }

    #endregion

}
