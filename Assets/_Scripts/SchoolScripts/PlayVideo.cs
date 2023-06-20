using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    public static bool headsetstart = false;
    public GameObject videoplayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(headsetstart == true)
        {
            videoplayer.SetActive(true);
            headsetstart = false;
            Invoke(nameof(stopvideo), 7);
        }

    }
    void stopvideo()
    {
        GameManager.Instance.isCutsceneOn = false;
        videoplayer.SetActive(false);
    }
}
