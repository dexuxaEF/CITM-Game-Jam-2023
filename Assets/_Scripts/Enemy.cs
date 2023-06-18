using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Info:")]
    [SerializeField]
    protected int health;

    [SerializeField]
    [Min(1f)] 
    protected float speed;

    [SerializeField]
    protected float radiusPlayerDetection = 1.1f;

    [SerializeField]
    [Tooltip("Damage done to this monster")]
    protected int getDamage;

    [SerializeField]
    protected float delayTimeToAttack = 2f;


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

    [Tooltip("Time between shoot")]
    protected float reloadTime = 1.0f;

    protected Queue<GameObject> projectilePool = new();
    protected GameObject projectileParent;

    protected GameObject player;
    protected Vector3 playerDirection;

    private AudioSource _audioSource;

    protected Vector3 nextPosition;
    protected Vector3 newDirection;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
        // Lógica básica de explosión común a todos los enemigos
        Destroy(gameObject);
    }

    public virtual void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }


    #region Move

    public virtual void Move()
    {
        if (Vector2.Distance(transform.position, nextPosition) < 0.1f || !PlayerOutOfNextPosition())
        {
            nextPosition = RandomPositionInsideBoundaries();
        }
        else
        {
            newDirection = (nextPosition - this.transform.position).normalized;
        }

        DebugNextPosition();

        //transform.Translate(playerDirection * speed*Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    bool PlayerOutOfNextPosition()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radiusPlayerDetection);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return false;
            }
        }

        return true;

    }

    Vector2 RandomPositionInsideBoundaries()
    {

        float x = RandomXInsideBoundaries();
        float y = RandomYInsideBoundaries();

        Vector2 newPosition = new Vector2(x, y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, newPosition - (Vector2)transform.position, Vector2.Distance(transform.position, newPosition));

        //if (hit.collider != null && hit.collider.CompareTag("Player"))
        //{

        //    return RandomPositionInsideBoundaries();
        //}

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                // El raycast chocó con el jugador, calcular otra posición
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusPlayerDetection);
    }
    #endregion

}
