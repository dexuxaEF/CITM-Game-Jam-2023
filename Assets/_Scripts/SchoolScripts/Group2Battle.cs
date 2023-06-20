using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Group2Battle : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.battle2win == true)
        {
            GameManager.Instance.isCutsceneOn = false;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.Instance.battle2win == false)
            {
                GameManager.Instance.isCutsceneOn = true;
                Invoke(nameof(StartBattle), 7);
                GameManager.Instance.battle2enter = true;
            }
        }
    }
    void StartBattle()
    {
        SceneManager.LoadScene("Combat2Scene");

    }
}
