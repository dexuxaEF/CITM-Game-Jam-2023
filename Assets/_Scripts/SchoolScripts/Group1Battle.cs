using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group1Battle : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.battle1win == true)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (GameManager.Instance.battle1win == false)
            {
                MoveCamera.isCutsceneOn = true;
                GameManager.Instance.battle1enter = true;
            }
        }
    }
}
