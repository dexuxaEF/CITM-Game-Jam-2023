using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEchoEffect : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public float destroyTime = 0.5f;

    public GameObject echo;
    private Rigidbody2D playerRB;

    private PlayerInCombat playerInCombat;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerInCombat = GetComponent<PlayerInCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInCombat.isDashing)
        {
            if (timeBtwSpawns <= 0)
            {
                Vector2 speedDirection = playerRB.velocity.normalized;
                GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.LookRotation(Vector3.forward, speedDirection));
                instance.transform.rotation = Quaternion.LookRotation(Vector3.forward, speedDirection);
                Destroy(instance, destroyTime);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }

    }


}
