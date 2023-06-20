using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Info:")]
    [SerializeField]
    protected int health;

    [SerializeField]
    [Min(0f)] 
    protected float speed;

    [SerializeField]
    [Tooltip("Damage done to this monster")]
    protected int getDamage;

    [SerializeField]
    protected float delayTimeToAttack = 2f;

    [SerializeField]
    [Tooltip("Time between each shoot")]
    protected float reloadTime = 1.0f;

    [SerializeField]
    protected float delayTimeToMove = 0.8f;

    [SerializeField]
    protected float stoppedTime = 0.2f;


    [Header("Map Info:")]
    [SerializeField]
    protected Transform leftBoundary;
    [SerializeField]
    protected Transform rightBoundary;
    [SerializeField]
    protected Transform topBoundary;
    [SerializeField]
    protected Transform bottomBoundary;


    [Header("Projectile:")]
    [SerializeField]
    protected GameObject projectilePrefab;

    [SerializeField]
    protected int initialPoolSize = 15;

    private GameObject playerobject;
    private PlayerInCombat _player;

    protected Queue<GameObject> projectilePool = new();
    protected GameObject projectileParent;

    protected GameObject player;
    protected Vector3 playerDirection;

    private AudioSource _audioSource;

    protected Vector3 nextPosition;
    protected Vector3 newDirection;

    private bool nextPositionCalculated = false;
    protected bool isMoving = false;

    [SerializeField]
    protected bool isStopped = false;

    [SerializeField]
    protected Animator _animator;



    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        playerobject = GameObject.FindWithTag("Player");
        _player = playerobject.GetComponent<PlayerInCombat>();
    }
    public virtual void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerInCombat>().gameObject;
        }

        nextPosition = this.transform.position;

    }


    
    public virtual void Attack()
    {
        //logica general ataque
    }


    public void Die()
    {
        
        Explode();
       
    }

    protected virtual void Explode()
    {
        // L�gica b�sica de explosi�n com�n a todos los enemigos
        Destroy(gameObject);
    }

    public virtual void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }


    #region Move

    public virtual void Move()
    {
        if (Vector2.Distance(transform.position, nextPosition) < 0.1f)
        {
            
            if(!nextPositionCalculated)
            {
                nextPositionCalculated = true;
                isMoving = false;
                Invoke(nameof(CalculateNextPosition), delayTimeToMove);

            }
        }
        else
        {
            isMoving = true;
            newDirection = (nextPosition - this.transform.position).normalized;
        }

        DebugNextPosition();

        //transform.Translate(playerDirection * speed*Time.deltaTime);
        if(!isStopped)
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    void CalculateNextPosition()
    {
        while(Vector2.Distance(transform.position, nextPosition) < 3f)
        {
            nextPosition = RandomPositionInsideBoundaries();
            nextPositionCalculated = false;
            
        }
    }   

    Vector2 RandomPositionInsideBoundaries()
    {   

        float x = RandomXInsideBoundaries();
        float y = RandomYInsideBoundaries();

        Vector2 newPosition = new Vector2(x, y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, newPosition - (Vector2)transform.position, Vector2.Distance(transform.position, newPosition));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                // El raycast choc� con el jugador, calcular otra posici�n
                return RandomPositionInsideBoundaries();
            }
        }


        return new Vector2(x, y);

    }

    float RandomXInsideBoundaries()
    {
        return Random.Range(leftBoundary.transform.position.x, rightBoundary.transform.position.x);
    }

    float RandomYInsideBoundaries()
    {
        return Random.Range(bottomBoundary.transform.position.y, topBoundary.transform.position.y);
    }


    public void AllowMovement()
    {
        isStopped = false;
    }


    #endregion

    #region debug

    void DebugNextPosition()
    {
        Debug.DrawLine(
        this.transform.position,
        nextPosition,
        Color.white,
        0.2f,
        true
        );

    }
    #endregion



}
