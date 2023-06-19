using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SadSceneEnter : MonoBehaviour
{
    public GameObject panel;
    public static bool startsad = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startsad == true && GameManager.Instance.hasteacherended == false)
        {
            SceneManager.LoadScene("SadScene");
            startsad = false;
        }
    }
}
