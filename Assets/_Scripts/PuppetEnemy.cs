using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetEnemy : Enemy
{

     private GameObject playerobject;
     private PlayerInCombat _player;

    public bool isCurved = true;

    public int lives = 2;

    [Tooltip("notDefaultAttack: kinda tracking projectile")]
    public bool defaultAttack = true;
    public bool invulnerability = false;




    private void Awake()
    {
        playerobject = GameObject.FindWithTag("Player");
        _player = playerobject.GetComponent<PlayerInCombat>();

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

        if(isMoving && isCurved && !isStopped) 
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

        Invoke(nameof(AllowMovement), stoppedTime);
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
            isStopped = true;
            yield return new WaitForSeconds(reloadTime);
            
        }
    }

    private void ChangeInvulnerability()
    {
        invulnerability = false;
    }
}
