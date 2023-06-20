using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Min(1.0f)] private float playerSpeed;

    private Rigidbody2D _rigidbody;

    public static Vector2 position;
    Vector2 previousposition;
    public bool up;
    public bool down;
    public bool right;
    public bool left;
    public bool idle;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        up = false;
        down = false;
        right = false;
        left = false;
        idle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.hasheadset == false) {
            if (_rigidbody.velocity.x == 0 && _rigidbody.velocity.x == 0 || idle == true)
            {
                gameObject.GetComponent<Animator>().SetBool("IsIdle", true);

            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("IsIdle", false);

            }
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("IsIdle", false);

            if (_rigidbody.velocity.x == 0 && _rigidbody.velocity.x == 0)
            {
                gameObject.GetComponent<Animator>().SetBool("HSidle", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("HSidle", false);

            }

        }
        if (_rigidbody.velocity.x >0 || right==true)
        {
            gameObject.GetComponent<Animator>().SetBool("IsRight", true);
            gameObject.GetComponent<Animator>().SetBool("IsIdle", false);
            gameObject.GetComponent<Animator>().SetBool("HSidle", false);

        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("IsRight", false);

        }
        if (_rigidbody.velocity.x < 0 || left==true)
        {
            gameObject.GetComponent<Animator>().SetBool("IsLeft", true);
            gameObject.GetComponent<Animator>().SetBool("IsIdle", false);
            gameObject.GetComponent<Animator>().SetBool("HSidle", false);

        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("IsLeft", false);
        }
        if (_rigidbody.velocity.y > 0 || up==true)
        {
            gameObject.GetComponent<Animator>().SetBool("IsUp", true);
            gameObject.GetComponent<Animator>().SetBool("IsIdle", false);
            gameObject.GetComponent<Animator>().SetBool("HSidle", false);

        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("IsUp", false);

        }
        if (_rigidbody.velocity.y < 0 || down == true)
        {
            gameObject.GetComponent<Animator>().SetBool("IsDown", true);
            gameObject.GetComponent<Animator>().SetBool("IsIdle", false);
            gameObject.GetComponent<Animator>().SetBool("HSidle", false);

        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("IsDown", false);
        }

        
        if (GameManager.Instance.isCutsceneOn == false)
        {
            Move();
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Stop();
        }
        if (GameManager.Instance.isCutsceneOn == true)
        {
            Stop();
        }
    }

    /// <summary>
    /// Move with coordinates
    /// </summary>
    //private void Move()
    //{
    //    float moveX = Input.GetAxisRaw("Horizontal");
    //    float moveY = Input.GetAxisRaw("Vertical");

    //    if (moveX != 0 || moveY != 0)
    //    {
    //        Vector3 movement = new Vector3(moveX, moveY).normalized;
    //        Vector3 newPosition = transform.position + movement * playerSpeed * Time.deltaTime;
    //        transform.position = newPosition;
    //    }
    //}

    /// <summary>
    /// Move with Physics
    /// </summary>
    private void Move()
    {
        Vector3 movement = new Vector3();

        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        if (movement.x > 0 && movement.y > 0)
            movement.Normalize();

        if (movement.magnitude > 0)
        {
            _rigidbody.velocity = movement * playerSpeed;
        }
    }
    private void Stop()
    {
        _rigidbody.velocity = new Vector3(0, 0, 0); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CorridorTrigger"))
        {
            GameManager.Instance.onCorridor = true;
        }
    }


}
