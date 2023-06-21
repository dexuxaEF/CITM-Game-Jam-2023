using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class NewParryScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject rippleParticles;
    private GameObject rippleParticleInstance;



    public Transform player;
    [Min(0f)] public float radius =0.5f;

    // Game Feel
    [SerializeField]
    private SlowMotion slowMoScript;
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener la posición del ratón en el mundo
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        // Calcular la dirección desde el jugador hacia la posición del ratón
        Vector3 direction = worldPos - player.position;
        direction.z = 0f; // Asegurarse de que el objeto no se incline hacia arriba o hacia abajo

        // Calcular el ángulo entre la dirección y el eje hacia arriba (Vector3.up)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Crear una rotación en función del ángulo
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Establecer la rotación del objeto
        transform.rotation = rotation;

        // Establecer la posición del objeto alrededor del jugador
        // Ajusta esto para controlar la distancia desde el jugador
        transform.position = player.position + (rotation * Vector3.right * radius);

        Debug.Log("a");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MouthProjectile") ||
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
