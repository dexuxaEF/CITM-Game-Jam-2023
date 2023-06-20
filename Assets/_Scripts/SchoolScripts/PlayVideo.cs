using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    public static bool headsetstart = false;
    public GameObject videoplayer;
    public Animator animat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (headsetstart == true)
        {
            Invoke(nameof(startvideo), 2);
            animat.SetBool("HS", true);
        }

    }
    void startvideo()
    {
        animat.SetBool("HS", false);
        animat.SetBool("IsIdle", false);
        GameManager.Instance.hasheadset = true;
        animat.SetBool("HSidle", true);
        videoplayer.SetActive(true);
        headsetstart = false;
        Invoke(nameof(stopvideo), 5);
    }
    void stopvideo()
    {
        GameManager.Instance.isCutsceneOn = false;
        GameManager.Instance.hascinematicended = true;
        videoplayer.SetActive(false);
    }
}
