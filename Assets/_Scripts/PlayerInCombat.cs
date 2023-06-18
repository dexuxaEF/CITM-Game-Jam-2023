using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInCombat : MonoBehaviour
{
    [SerializeField] [Min(1.0f)] private float playerSpeed=1f;

    private Rigidbody2D _rigidbody;
    public GameObject _parry;

    [SerializeField]
    public float timeParryAvailable = 0.35f;

    [Header("Dash")]
    //Fuerza del DASH
    [SerializeField] [Min(5f)] private float DashForce =5f;
    //Tiempo que dura el DASH
    [SerializeField] [Min(0f)] private float DashTime =0.2f;
    //Tiempo que te tienes que esperar para volver a usar el DASH
    [SerializeField] [Min(0.1f)] private float DashCooldown=1f;         


    //Mientras esta varialbe sea True el personaje estar haciendo el DASH
    private bool isDashing;
    //Nos permite saber si tiene el dash disponible
    private bool canDash=true;

    [Header("KnockBack")]
    [SerializeField] [Min(0.01f)] private float iframes=1f;
    [HideInInspector]
    public bool invulnerability;
    [SerializeField] [Min(0.01f)] private float KnockBackForce = 5f;
    [SerializeField] [Min(0.01f)] private float knockbackTime=0.1f;

    [HideInInspector]
    public bool isKnockBack= false; 

    //LaDireccion a la que te quieres mover
    [HideInInspector]
    public Vector3 direction;

    [Min(0.01f)] public float parryacceleration =1.50f;

    public int lives;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        invulnerability = false;
        isDashing = false;
        isKnockBack = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        Rotate();

        if (!isDashing && !isKnockBack)
        {
            Move();
            if (Input.GetMouseButtonDown(0))
            {
                if (!_parry.activeInHierarchy)
                {
                    _parry.SetActive(true);
                    Invoke(nameof(DesactivateParry), timeParryAvailable);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isKnockBack)
        {
            StartCoroutine(Dash());
        }

        //if (Input.GetKeyDown(KeyCode.Z) &&  !isKnockBack)
        //{
        //    StartCoroutine(Knockback(new Vector3(2,2)));
        //}


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
    //private void Move()
    //{
    //    float moveX = Input.GetAxisRaw("Horizontal");
    //    float moveY = Input.GetAxisRaw("Vertical");

    //    if (moveX != 0 || moveY != 0)
    //    {
    //        Vector2 movement = new Vector2(moveX, moveY).normalized;
    //        Vector2 newPosition = transform.position + movement * playerSpeed * Time.deltaTime;
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

    private void DesactivateParry()
    {
        _parry.SetActive(false);
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        iframes = DashTime;
        invulnerability = true;
        if (direction.x != 0 && direction.y != 0)
        {
            _rigidbody.velocity = new Vector2(direction.x * DashForce * 0.5f, direction.y * DashForce * 0.5f);
        }
        else
        {
            _rigidbody.velocity = new Vector2(direction.x * DashForce, direction.y * DashForce);
        }
        yield return new WaitForSeconds(DashTime);
        invulnerability = false;
        isDashing = false;
        yield return new WaitForSeconds(DashCooldown);
        canDash = true;

    }   

    public IEnumerator Knockback(Vector3 dir)
    {
        
        lives--;
        iframes = 1f;
        invulnerability = true;
        isKnockBack = true;

        _rigidbody.velocity = new Vector2(dir.x* KnockBackForce, dir.y* KnockBackForce);

        yield return new WaitForSeconds(knockbackTime);
        _rigidbody.velocity = new Vector2(0, 0);
        isKnockBack = false;
        yield return new WaitForSeconds(iframes);   
        invulnerability = false;


    }

    public void KnockBack2(Vector3 dir)
    {
        invulnerability = true;
        isKnockBack = true;
        _rigidbody.velocity = new Vector2(0, 0);
        _rigidbody.AddForce(dir * KnockBackForce, ForceMode2D.Impulse);
        Invoke(nameof(Explsion), knockbackTime);
        Invoke(nameof(Invulnerability), iframes);   

    }

    private void Explsion()
    {
        _rigidbody.velocity = new Vector2(0, 0);
        isKnockBack = false;
    }

    private void Invulnerability() 
    {
        invulnerability = false;

    }


}
