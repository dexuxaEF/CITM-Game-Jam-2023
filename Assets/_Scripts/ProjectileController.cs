using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField][Min(1.0f)] public float speed;

    public Vector3 direction;
    private GameObject playerobject;
    private Rigidbody2D _rigidbody;
    private PlayerInCombat player;

    private float maxDuration = 10f, currentDuration = 0f;
    private bool parryTrigger = false;
  
    private void Awake()
    {
       
        _rigidbody = GetComponent<Rigidbody2D>();
        playerobject = GameObject.FindWithTag("Player");
        player = playerobject.GetComponent<PlayerInCombat>();
    }

    private void FixedUpdate()
    {

        if (!parryTrigger)
        {
            _rigidbody.velocity = speed * direction;
        }


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
            

            Vector2 wallNormal = collision.transform.up;

            direction = Vector2.Reflect(direction, wallNormal).normalized;
        }


        if(collision.gameObject.CompareTag("Parry"))
        {
            if (!parryTrigger)
            {
                parryTrigger = true;
                direction = collision.transform.up;
                _rigidbody.velocity = new Vector2(0, 0);
                Invoke(nameof(Parry), 0.1f);
            }

        }

        if (collision.gameObject.CompareTag("Player"))
        {

            
            if (!player.invulnerability)
            {
              
               Vector3 dir = (direction).normalized;
                //StartCoroutine( player.Knockback(dir));
               player.KnockBack(dir);
               gameObject.SetActive(false);
              
              

            }

        }
        

    }


    private void Parry()
    {
        speed = speed * player.parryacceleration;
        parryTrigger = false;
    }

}
