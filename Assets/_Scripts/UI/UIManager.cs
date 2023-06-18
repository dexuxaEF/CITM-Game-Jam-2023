using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Fade fadeScript;

    public string playSceneName;
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void PlayGame()
    {
        StartCoroutine(WaitForFadeToEnd());
               
    }

    IEnumerator WaitForFadeToEnd()
    {
        fadeScript.FadeOut();

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(playSceneName);
    }


}
