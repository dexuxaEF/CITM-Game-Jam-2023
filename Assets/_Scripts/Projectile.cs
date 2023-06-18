using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected int maxWallBounces;

    [SerializeField]
    protected int defaultMaxWallBounces;
    

    [SerializeField]
    public float speed;

    private AudioSource _audioSource;

    [HideInInspector]
    public Vector3 direction;


    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected abstract void Move();

    public virtual void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    void SpawnVFX(GameObject vfx)
    {

    }

}
