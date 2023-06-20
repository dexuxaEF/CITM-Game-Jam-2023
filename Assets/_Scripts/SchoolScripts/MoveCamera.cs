using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
   
    public Animator camAnim;
    public static bool videostart;
    float count = -1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            count = 0;
            GameManager.Instance.doorcloser = true;
        }
    }
    void Update()
    {
        if(GameManager.Instance.hascinematicended == true)
        {
            Destroy(gameObject);
        }
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
            PlayVideo.headsetstart = true;
            camAnim.SetBool("CutSceneGroup1", false);
            Destroy(gameObject);
        }
    }

}
