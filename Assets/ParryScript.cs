using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ParryScript : MonoBehaviour
{
    [SerializeField]
    private GameObject rippleParticles;
    private GameObject rippleParticleInstance;

    // Game Feel
    [SerializeField]
    private SlowMotion slowMoScript;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MouthProjectile") ||
           collision.gameObject.CompareTag("HeartProjectile") ||
           collision.gameObject.CompareTag("PuppetProjectile"))
        {
            rippleParticleInstance = Instantiate(rippleParticles, transform.position, Quaternion.identity);
            Destroy(rippleParticleInstance, .5f);
            // Game feel
            CameraShaker.Instance.ShakeOnce(1.0f, 1.0f, 0f, 1.0f);
            //slowMoScript.StartDamageSlowMo(.2f);

        }
    }
}
