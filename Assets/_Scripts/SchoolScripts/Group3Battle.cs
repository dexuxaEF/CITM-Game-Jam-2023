using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group3Battle : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.battle3win == true)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (GameManager.battle3win == false)
            {
                GameManager.battle3enter = true;
            }
        }
    }
}
