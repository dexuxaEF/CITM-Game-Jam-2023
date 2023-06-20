using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ParryScript : MonoBehaviour
{
    [SerializeField]
    private GameObject rippleParticles;
    private GameObject rippleParticleInstance;

    private GameObject playerobject;
    private PlayerInCombat player;
    public float distance;

    // Game Feel
    [SerializeField]
    private SlowMotion slowMoScript;

    void Start()
    {
        playerobject = GameObject.FindWithTag("Player");
        player = playerobject.GetComponent<PlayerInCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position + player.mousedirection * distance;
        Quaternion newRotation = Quaternion.LookRotation(player.mousedirection, Vector3.up);

        // Aplicar la nueva posición y orientación al elemento
        this.transform.position = newPosition;
        this.transform.rotation = newRotation;
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
            slowMoScript.StartDamageSlowMo(.2f);

        }
    }
}
