using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    [HideInInspector]
    public float destroyTime = 0.5f;

    public GameObject echo;
    private ProjectileController projectile;
    private Rigidbody2D projectileRB;
    private void Start()
    {
        projectileRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        if(projectileRB.velocity != Vector2.zero)
        {
            if (timeBtwSpawns <= 0)
            {
                Vector2 speedDirection = projectileRB.velocity.normalized;
                GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.LookRotation(Vector3.forward, speedDirection));
                Destroy(instance, destroyTime);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Reduce the wave size
        if (collision.gameObject.CompareTag("Wall"))
        {
            startTimeBtwSpawns += 0.04f;
        }
    }

    public void RestartWaveCount()
    {
        startTimeBtwSpawns = 0.05f;
    }

}
