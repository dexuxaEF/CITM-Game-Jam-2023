using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInCombat : MonoBehaviour
{
    [SerializeField] [Min(1.0f)] private float playerSpeed;

    private Rigidbody2D _rigidbody;
    public GameObject _parry;

    [SerializeField]
    public float timeParryAvailable = 0.35f;

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
        Rotate();
        Move();

        if(Input.GetMouseButtonDown(0))
        {
            if(!_parry.activeInHierarchy)
            {
                _parry.SetActive(true);
                Invoke(nameof(DesactivateParry), timeParryAvailable);
            }
        }

    }

    void Rotate()
    {
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        Vector3 playerPos = this.transform.position;

        //Distancia de player al rat�n
        float distanceX = playerPos.x - worldPos.x;
        float distanceY = playerPos.y - worldPos.y;

        //Calculamos el angulo que hay entre distanceX y distanceY para saber cuanto tiene que rotar
        float angle = Mathf.Atan2(distanceY, distanceX) * Mathf.Rad2Deg; //Mathf siempre fuciona con radianes asi que multiplicamos por Rad2Deg

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90)); //+90 por la diferencia de donde se empiezan a contar los angnulos, unity empieza en verticla y el calculo que hemos hecho
                                                                          //de arctan2 desde el horizontal

        this.transform.rotation = rot;


    }
    /// <summary>
    /// Move with coordinates
    /// </summary>
    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            Vector3 movement = new Vector3(moveX, moveY).normalized;
            Vector3 newPosition = transform.position + movement * playerSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    /// <summary>
    /// Move with Physics
    /// </summary>
    //private void Move()
    //{
    //    Vector3 movement = new Vector3();

    //    movement.x += Input.GetAxis("Horizontal");
    //    movement.y += Input.GetAxis("Vertical");

    //    if (movement.x > 0 && movement.y > 0)
    //        movement.Normalize();

    //    if (movement.magnitude > 0)
    //    {
    //        _rigidbody.velocity = movement * playerSpeed;
    //    }
    //}

    private void DesactivateParry()
    {
        _parry.SetActive(false);
    }

}
