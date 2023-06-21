using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthEnemy : Enemy
{
    private GameObject playerobject;
    private PlayerInCombat _player;
    private GameObject projectileobject;
    private CircleProjectile projectile;

    public AudioSource hitSFX;
    public AudioSource attackSFX;
    public int minNumProjectiles = 3;
    public int maxNumProjectiles = 6;
    public int lives = 2;
    private int previouslives;
    [Tooltip("Area to spawn projectiles")]
    public float totalAngle = 360f;

    public bool invulnerability = false;
    public bool isKnockBack = false;

    private void Awake()
    {
        playerobject = GameObject.FindWithTag("Player");
        _player = playerobject.GetComponent<PlayerInCombat>();


    }
    void Start()
    {
        base.Start();

        projectileParent = new GameObject("MouthProjectileParent");

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileParent.transform);
            projectile.SetActive(false);
            projectilePool.Enqueue(projectile);

        }

        playerDirection = (player.transform.position - this.transform.position);
        Invoke(nameof(CoroutineWithDelay), delayTimeToAttack);

        previouslives = lives;
    }

    
    void Update()
    {

        if (previouslives > lives)
        {
            hitSFX.Play();
            previouslives = lives;
        }
        if (lives <= 0)
        {
            _player.win = true;
            Die();

        }

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
        //    SpawnProjectile();
        //}

    }

    public void SpawnProjectile()
    {
        int projectilesToSpawn = Random.Range(minNumProjectiles, maxNumProjectiles+1);
        float angleBetweenProjectile = totalAngle / projectilesToSpawn;

        for (int i = 0; i < projectilesToSpawn; i++)
        {

            GameObject projectile = projectilePool.Dequeue();
            if (projectile != null)
            {
                projectile.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
                CircleProjectile projectileBehavior = projectile.GetComponent<CircleProjectile>();
                projectile.SetActive(true);

                //Para darle un poco más de variabilidad y que no siempre spawneen en el mismo lugar si salen que tienen q spawnear, 3,4 o 5...
                float randomAngle = Random.Range(0, 45+1);

                Quaternion angle = Quaternion.Euler(0f, 0f, angleBetweenProjectile * (i + 1) + randomAngle);

                projectileBehavior.direction = angle * playerDirection;
                

            }
            projectilePool.Enqueue(projectile);

        }

        Invoke(nameof(AllowMovement), stoppedTime);

    }


    //Función proyectil



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
            attackSFX.Play();
            SpawnProjectile();
            isStopped = true;

            yield return new WaitForSeconds(reloadTime);
        }
    }



}
