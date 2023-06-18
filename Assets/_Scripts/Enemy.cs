using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;

    [SerializeField]
    protected float speed;

    [SerializeField]
    [Tooltip("Damage done to this monster")]
    protected int getDamage;

    [SerializeField]
    protected float delayTimeToAttack = 2f;

    [Header("Projectile")]
    [SerializeField]
    protected GameObject projectilePrefab;
    [SerializeField]
    protected int initialPoolSize = 15;

    [Tooltip("Time between shoot")]
    protected float reloadTime = 1.0f;

    protected Queue<GameObject> projectilePool = new();
    protected GameObject projectileParent;

    protected GameObject player;
    protected Vector3 playerDirection;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public virtual void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerInCombat>().gameObject;
        }
        
    }

    public virtual void Move()
    {
     //logica general move   
    }

    public virtual void Attack()
    {
        //logica general ataque
    }


    public void Die()
    {

        Explode();
    }

    protected virtual void Explode()
    {
        // Lógica básica de explosión común a todos los enemigos
        Destroy(gameObject);
    }

    public virtual void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

}
