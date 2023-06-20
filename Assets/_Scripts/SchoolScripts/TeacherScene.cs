using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherScene : MonoBehaviour
{
    public static bool teacherscene;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        teacherscene = false;
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            teacherscene = true;
            if(GameManager.Instance.hasteacherended == false)
            audio.Play();
        }
    }
    
}
