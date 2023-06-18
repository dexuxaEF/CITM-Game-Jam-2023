using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Min(1.0f)] private float playerSpeed;

    private Rigidbody2D _rigidbody;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveCamera.isCutsceneOn == false)
        {
            Move();
        }
        if (MoveCamera.isCutsceneOn == true)
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


}
