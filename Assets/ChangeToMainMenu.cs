using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToMainMenu : MonoBehaviour
{
    public string nextSceneName;

    // When  clik 2
    private int clicks = 0;

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            clicks++;
        }

        if(clicks >= 2)
        {
            GameManager.Instance.RestartVariables();
            SceneManager.LoadScene(nextSceneName);
            clicks = 0;
        }
    }
}
