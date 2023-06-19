using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndIntro : MonoBehaviour
{
    public static bool hasended = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hasended == true)
        {
            SceneManager.LoadScene("School");
        }
    }
}
