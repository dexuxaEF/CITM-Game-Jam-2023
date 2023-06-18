using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject classdoor;
    void Start()
    {
        classdoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(MoveCamera.isCutsceneOn == false)
        {
            classdoor.SetActive(true);
        }
    }
}
