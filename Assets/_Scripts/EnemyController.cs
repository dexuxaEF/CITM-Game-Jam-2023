using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public GameObject projectilePrefab;

    private GameObject player;

    [SerializeField]
    private float reloadTime = 4f, counter = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
        player = FindObjectOfType<PlayerInCombat>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {



        counter += Time.deltaTime;
        if(counter > reloadTime)
        {
            GameObject projectile = GameObject.Instantiate(projectilePrefab, this.transform.position, Quaternion.identity) as GameObject;
            Vector2 direction = (player.transform.position - this.transform.position).normalized;
            projectile.GetComponent<ProjectileController>().direction = direction;

            counter = 0f;
        }

    }
}
