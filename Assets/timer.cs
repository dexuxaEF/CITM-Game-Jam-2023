using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    
    public GameObject timerRaton;

    public  float timerTiempo;

    private int mouseClick;

    // Start is called before the first frame update
    void Start()
    {
        
        Invoke(nameof(PaActivarlo), timerTiempo);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mouseClick++;
        }

        if (mouseClick >= 2)
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    void PaActivarlo()
    {
        timerRaton.SetActive(true);
    }
}
