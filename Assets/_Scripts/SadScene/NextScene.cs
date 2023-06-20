using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(nextscene), 11);
    }
    void nextscene()
    {
        GameManager.Instance.hasteacherended = true;
        SceneManager.LoadScene("School");
    }
}
