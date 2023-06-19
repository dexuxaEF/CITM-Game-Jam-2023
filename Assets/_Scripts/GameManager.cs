using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool hasteacherended;
    public bool battle1enter;
    public bool battle1win;
    public bool battle2enter;
    public bool battle2win;
    public bool battle3enter;
    public bool battle3win;
    public bool isCutsceneOn = true;
    public bool doorcloser = false;
    bool timer = false;
    private void Awake()
    {

      if (Instance == null)
      {
          Instance = this;
          DontDestroyOnLoad(gameObject);
      }
      else
      {
          Destroy(gameObject);
      }
      

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

}
