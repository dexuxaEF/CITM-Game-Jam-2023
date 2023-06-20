using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Group1Battle : MonoBehaviour
{

    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.battle1win == true)
        {
            GameManager.Instance.isCutsceneOn = false;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (GameManager.Instance.battle1win == false)
            {
                audio.Play();
                GameManager.Instance.isCutsceneOn = true;
                Invoke(nameof(StartBattle), 6);
                GameManager.Instance.battle1enter = true;
            }
        }
    }
    void StartBattle()
    {
        SceneManager.LoadScene("Combat1Scene");

    }
}
