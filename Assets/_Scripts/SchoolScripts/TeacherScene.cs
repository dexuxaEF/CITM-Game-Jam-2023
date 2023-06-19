using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherScene : MonoBehaviour
{
    public static bool teacherscene;
    // Start is called before the first frame update
    void Start()
    {
        teacherscene = false;
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
        }
    }
    
}
