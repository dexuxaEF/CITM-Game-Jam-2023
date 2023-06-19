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

    [SerializeField]
    public float defaultSpeed;

    private AudioSource _audioSource;

    [HideInInspector]
    public Vector3 direction;

    public EchoEffect echoEffect;


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

    public void DefaultSpeed()
    {
        speed = defaultSpeed;
        
    }

    private void OnEnable()
    {
        DefaultSpeed();

        // Restart Wave count
        echoEffect.RestartWaveCount();
    }

}
