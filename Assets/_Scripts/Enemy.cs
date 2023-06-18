using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;

    [SerializeField]
    protected float speed;

    protected GameObject player;

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
