using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.SceneManagement;
using System;

public class PlayerInCombat : MonoBehaviour
{
    [Header("Game Feel")]
    // Game Feel
    [SerializeField]
    private SlowMotion slowMoScript;
    [SerializeField]
    private GameObject bloodParticles;
    private GameObject bloodParticleInstance;

    [Header("Player")]
    [HideInInspector]
    [SerializeField] [Min(1.0f)] private float playerSpeed =10f;
    [Min(0f)] public float lives= 3f;
    private float previouslives;
    [HideInInspector]
    private Rigidbody2D _rigidbody;

   

    public GameObject _newparry;
    

    public ChargeBar chargeBar;
    public GameObject _chargeBarObject;

    public GameObject HudLive1;
    public GameObject HudLive2;
    public GameObject HudLive3;
    public GameObject HudLive4;

    [SerializeField]
    public float timeParryAvailable = 0.35f;

    [Header("Dash")]
    //Fuerza del DASH
    [SerializeField] [Min(5f)] private float DashForce =15f;
    //Tiempo que dura el DASH
    [SerializeField] [Min(0f)] private float DashTime =0.2f;
    //Tiempo que te tienes que esperar para volver a usar el DASH
    [SerializeField] [Min(0.1f)] private float DashCooldown=0.5f;
    [SerializeField] [Min(0.1f)] private float IframesDash = 2f;
    public AudioSource dashSFX;
    public AudioSource hurtSFX;


    //Mientras esta varialbe sea True el personaje estar haciendo el DASH
    [HideInInspector]
    public bool isDashing;
    //Nos permite saber si tiene el dash disponible
    private bool canDash=true;
    [HideInInspector]
    public bool win = false;
    [HideInInspector]
    public bool lose = false;

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
    public float maxparrycooldown = 1f;
    private bool isparry = false;
    private bool canParry = true;
    private float parrycooldown;

    [HideInInspector]
    public Vector3 mousedirection;

  



    string nombreEscena;
    // Obtener el nombre de la escena actual


    public Animator _animator;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        

    }

    // Start is called before the first frame update
    void Start()
    {
        lives = 4;
        invulnerability = false;
        isDashing = false;
        isKnockBack = false;
        chargeBar.SetCharge(0);
        chargeBar.SetMaxCharge(maxparrycooldown);
        canParry = true;
        nombreEscena = SceneManager.GetActiveScene().name;
        previouslives = lives;
    
    }

    // Update is called once per frame
    void Update()
    {

        if (previouslives > lives)
        {
            hurtSFX.Play();
            previouslives = lives;
        }


        Vector2 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);
        mousedirection = (worldPos - _rigidbody.position).normalized;

        if (win == true)
        {
            win = false;
            if(nombreEscena == "Combat1Scene")
            {
                GameManager.Instance.battle1win = true;
            }
            if (nombreEscena == "Combat2Scene")
            {
                GameManager.Instance.battle2win = true;
            }
            if (nombreEscena == "Combat3Scene")
            {
                GameManager.Instance.battle3win = true;
            }
            Invoke(nameof(changescene), 1);

        }
        if(lose == true)
        {
            lose = false;
            if (nombreEscena == "Combat1Scene")
            {
                GameManager.Instance.battle1lost = true;
            }
            if (nombreEscena == "Combat2Scene")
            {
                GameManager.Instance.battle2lost = true;
            }
            if (nombreEscena == "Combat3Scene")
            {
                GameManager.Instance.battle3lost = true;
            }

            Invoke(nameof(changescene), 1);
        }

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        UpdateAnimations(direction.x, direction.y);
        //Rotate();

        if (!isDashing && !isKnockBack)
        {
            Move();
            if (Input.GetMouseButtonDown(0) && canParry )
            {
                if (!_newparry.activeInHierarchy )
                {
                  
                    _newparry.SetActive(true);
                    canParry = false;
                    playerSpeed = 0;
                    Invoke(nameof(DesactivateParry), timeParryAvailable);
                    Invoke(nameof(NormalSpeed), 0.5f);
                }
            }   
        }

        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isKnockBack)
        {
            dashSFX.Play();
            StartCoroutine(Dash());
        }

        if (invulnerabilityParry) 
        {
            Invoke(nameof(Invulnerability), invulnerabilityParryTimer);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            chargeBar.SetCharge(2);
        }

        if (isparry)
        {

            parrycooldown +=Time.deltaTime;
            chargeBar.SetCharge(parrycooldown);
            CheckCooldown();
        }

        CheckLose();
        UpdateLiveHud();

    }

    private void UpdateAnimations(float x, float y)
    {

        if (x > 0.1)
        {
            _animator.SetBool("Right", true);


            _animator.SetBool("Left", false);
            _animator.SetBool("Up", false);
            _animator.SetBool("Down", false);
            _animator.SetBool("Idle", false);

        }
        else if (x < -0.1)
        {
            _animator.SetBool("Left", true);

            _animator.SetBool("Right", false);
            _animator.SetBool("Up", false);
            _animator.SetBool("Down", false);
            _animator.SetBool("Idle", false);
        }
        else if (y > 0.1)
        {
            _animator.SetBool("Up", true);

            _animator.SetBool("Right", false);
            _animator.SetBool("Left", false);
            _animator.SetBool("Down", false);
            _animator.SetBool("Idle", false);
        }
        else if (y < -0.1)
        {
            _animator.SetBool("Down", true);


            _animator.SetBool("Right", false);
            _animator.SetBool("Left", false);
            _animator.SetBool("Up", false);
            _animator.SetBool("Idle", false);
        }
        else
        {
            _animator.SetBool("Idle", true);

            _animator.SetBool("Right", false);
            _animator.SetBool("Left", false);
            _animator.SetBool("Up", false);
            _animator.SetBool("Down", false);
        }

     


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Trigger con el Proyectil del Player
        if (collision.gameObject.CompareTag("Puppet"))
        {

            if (!invulnerability)
            {

                lives--;
                Vector3 dir = this.transform.up;

                KnockBack(dir);


            }
        }
        if (collision.gameObject.CompareTag("Heart"))
        {

            if (!invulnerability)
            {

                lives--;
                Vector3 dir = this.transform.up;

                KnockBack(dir);


            }
        }
        if (collision.gameObject.CompareTag("Mouth"))
        {

            if (!invulnerability)
            {

                lives--;
                Vector3 dir = this.transform.up;

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
        
        _newparry.SetActive(false);
        _chargeBarObject.SetActive(true);
        isparry = true;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        
        invulnerability = true;
        if (direction.x != 0 && direction.y != 0)
        {
            _rigidbody.velocity = new Vector2(direction.x * DashForce * 0.5f, direction.y * DashForce * 0.5f);
        }
        else
        {
            _rigidbody.velocity = new Vector2(direction.x * DashForce, direction.y * DashForce);
        }
        Invoke(nameof(Invulnerability), IframesDash);
        yield return new WaitForSeconds(DashTime);
        isDashing = false;
        yield return new WaitForSeconds(DashCooldown);
        canDash = true;

    }   

    public void KnockBack(Vector3 dir)
    {
        invulnerability = true;
        isKnockBack = true;
        lives--;
        _rigidbody.velocity = new Vector2(0, 0);
        _rigidbody.AddForce(dir * KnockBackForce, ForceMode2D.Impulse);
        Invoke(nameof(Explsion), knockbackTime);
        Invoke(nameof(Invulnerability), iframes);

        // Game feel
        CameraShaker.Instance.ShakeOnce(5.0f, 5.0f, 0f, 1.0f);
        slowMoScript.StartDamageSlowMo(1.0f);
        bloodParticleInstance = Instantiate(bloodParticles, transform.position, Quaternion.identity);
        Destroy(bloodParticleInstance, 1);
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

    private void CheckCooldown()    
    {
        if (parrycooldown >= maxparrycooldown)
        {
            canParry = true;
            _chargeBarObject.SetActive(false);
            parrycooldown = 0f;
            isparry = false;
        }
  
    }

    private void UpdateLiveHud()
    {
        if (lives == 4)
        {
            HudLive1.SetActive(true);
            HudLive2.SetActive(false);
            HudLive3.SetActive(false);
            HudLive4.SetActive(false);
        }
        if (lives == 3)
        {
            HudLive1.SetActive(false);
            HudLive2.SetActive(true);
            HudLive3.SetActive(false);
            HudLive4.SetActive(false);
        }
        if (lives == 2)
        {
            HudLive1.SetActive(false);
            HudLive2.SetActive(false);
            HudLive3.SetActive(true);
            HudLive4.SetActive(false);
        }
        if (lives == 1)
        {
            HudLive1.SetActive(false);
            HudLive2.SetActive(false);
            HudLive3.SetActive(false);
            HudLive4.SetActive(true);
        }
    }

    private void CheckLose()
    {
        if (lives <= 0)
        {
            lose = true;
            //if()
        }
    }
    void changescene()
    {
        SceneManager.LoadScene("School");
    }
}
