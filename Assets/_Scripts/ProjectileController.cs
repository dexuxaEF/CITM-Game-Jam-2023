using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ProjectileController : MonoBehaviour
{
    [SerializeField][Min(1.0f)] public float speed;

    public Vector3 direction;

    public Rigidbody2D _rigidbody;

    private float maxDuration = 10f, currentDuration = 0f;
    private void Awake()
    {
       
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {


        _rigidbody.velocity = speed * direction;



        currentDuration += Time.deltaTime;

        if (currentDuration > maxDuration)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Wall in");
            Vector2 wallNormal = collision.transform.up;

            direction = Vector2.Reflect(direction, wallNormal).normalized;

            // Trigger screen shake when ball colliding with wall
            // For game feel
            CameraShaker.Instance.ShakeOnce(1f, 1.5f, .1f, .1f);
        }


        if(collision.gameObject.CompareTag("Parry"))
        {
            direction = collision.transform.up;
        }


    }


}
