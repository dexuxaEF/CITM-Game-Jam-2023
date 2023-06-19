using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Group3Battle : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.battle3win == true)
        {
            GameManager.Instance.isCutsceneOn = false;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (GameManager.Instance.battle3win == false)
            {
                GameManager.Instance.isCutsceneOn = true;
                Invoke(nameof(StartBattle), 7);
                GameManager.Instance.battle3enter = true;
            }
        }
    }
    void StartBattle()
    {
        //SceneManager.LoadScene("Combat3Scene");

    }
}
