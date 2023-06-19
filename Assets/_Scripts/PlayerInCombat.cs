using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerInCombat : MonoBehaviour
{
    [Header("Game Feel")]
    // Game Feel
    [SerializeField]
    private SlowMotion slowMoScript;
    [SerializeField]
    private GameObject bloodParticles;

    [Header("Player")]
    [HideInInspector]
    [SerializeField] [Min(1.0f)] private float playerSpeed =10f;
    [Min(0f)] public float lives= 3f;

    [HideInInspector]
    private Rigidbody2D _rigidbody;

    public GameObject _parry;

    [SerializeField]
    public float timeParryAvailable = 0.35f;

    [Header("Dash")]
    //Fuerza del DASH
    [SerializeField] [Min(5f)] private float DashForce =10f;
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

    [Header("Parry")]
    [Min(0.01f)] public float parryacceleration =1.50f;
    [Min(0f)] public float invulnerabilityParryTimer =0.5f;
    [HideInInspector]
    public bool invulnerabilityParry = false;


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
                    playerSpeed = 0;
                    Invoke(nameof(DesactivateParry), timeParryAvailable);
                    Invoke(nameof(NormalSpeed), 0.5f);
                }
            }   
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isKnockBack)
        {
            StartCoroutine(Dash());
        }

        if (invulnerabilityParry) 
        {
            Invoke(nameof(Invulnerability), invulnerabilityParryTimer);
        }
    


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Trigger con el Proyectil del Player
        if (collision.gameObject.CompareTag("Enemys"))
        {

            if (!invulnerability)
            {

                lives--;
                Vector3 dir = collision.transform.up;
              
                KnockBack(dir);
           

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
        else
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
        iframes = 1f;
        invulnerability = true;
        if (direction.x != 0 && direction.y != 0)
        {
            _rigidbody.velocity = new Vector2(direction.x * DashForce * 0.5f, direction.y * DashForce * 0.5f);
        }
        else
        {
            _rigidbody.velocity = new Vector2(direction.x * DashForce, direction.y * DashForce);
        }
        Invoke(nameof(Invulnerability), iframes);
        yield return new WaitForSeconds(DashTime);
        isDashing = false;
        yield return new WaitForSeconds(DashCooldown);
        canDash = true;

    }   

    public void KnockBack(Vector3 dir)
    {
        invulnerability = true;
        isKnockBack = true;
        _rigidbody.velocity = new Vector2(0, 0);
        _rigidbody.AddForce(dir * KnockBackForce, ForceMode2D.Impulse);
        Invoke(nameof(Explsion), knockbackTime);
        Invoke(nameof(Invulnerability), iframes);

        // Game feel
        CameraShaker.Instance.ShakeOnce(5.0f, 5.0f, 0f, 1.0f);
        slowMoScript.StartDamageSlowMo(1.0f);
        Instantiate(bloodParticles, transform.position, Quaternion.identity);
        //Destroy(gameObject);

    }

    private void Explsion()
    {
        _rigidbody.velocity = new Vector2(0, 0);
        isKnockBack = false;
    }

    public void Invulnerability() 
    {
       invulnerability = false;
       invulnerabilityParry = false;

    }

    public void NormalSpeed() 
    {
        playerSpeed = 5;
    
    }


}
