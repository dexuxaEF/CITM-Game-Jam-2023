using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public static bool isCutsceneOn = true;
    public Animator camAnim;
    float count = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            count = 0;
        }
    }
    void Update()
    {

        if(count >= 0)
        {
            count += Time.deltaTime;
        }
        if(count >= 2)
        {
            camAnim.SetBool("CutSceneGroup1", true);
        }
        if (count >= 6)
        {
            isCutsceneOn = false;
            camAnim.SetBool("CutSceneGroup1", false);
            Destroy(gameObject);
        }
    }

}