using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool battle1enter;
    public static bool battle1win;
    public static bool battle2enter;
    public static bool battle2win;
    public static bool battle3enter;
    public static bool battle3win;
    bool timer = false;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        battle1enter = false;
        battle1win = false;
        battle2enter = false;
        battle2win = false;
        battle3enter = false;
        battle3win = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(battle1enter == true)
        {
            timer = true;
        }
        if (timer == true)
        {
            Invoke(nameof(startbattle1), 4);
            timer = false;
        }
    }
    void startbattle1()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
