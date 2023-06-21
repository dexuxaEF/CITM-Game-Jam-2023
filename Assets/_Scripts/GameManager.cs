using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool hasteacherended;
    public bool onCorridor;

    public bool battle1enter;
    public bool battle1win;
    public bool battle2enter;
    public bool battle2win;
    public bool battle3enter;
    public bool battle3win;

    public bool battle1lost;
    public bool battle2lost;
    public bool battle3lost;

    public bool isCutsceneOn = true;
    public bool doorcloser = false;
    public bool hascinematicended = false;
    public bool hasheadset = false;

    bool prueba = false;

    public PostProcessProfile postProcessingProfile;
    private ColorGrading colorGrading;

    public SlowMotion slowmo;

    public float newSaturation;



   // [HideInInspector]
   // public int lostBattleCount;

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

        postProcessingProfile.TryGetSettings(out colorGrading);
        float initialSaturation = colorGrading.saturation.value;

        newSaturation = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Combat1Scene")
        {
            StopSong();        
        }
        if (SceneManager.GetActiveScene().name == "Combat2Scene")
        {
            StopSong();
        }
        if (SceneManager.GetActiveScene().name == "Combat3Scene")
        {
            StopSong();
        }
        if (SceneManager.GetActiveScene().name == "School")
        {
            //Time.timeScale = 1.0f;
            PlaySong();
           
        }
        if (SceneManager.GetActiveScene().name == "CreditsScene")
        {
            StopSong();
        }

        if (battle1lost || battle2lost || battle3lost)
        {
            isCutsceneOn = false;
            doorcloser = true;
            hascinematicended = true;
            hasheadset = true;

            prueba = true;
        }




        // Post process when battle is won
        // Changes the saturation :|
        ChangeSaturation();


    }
    public void PlaySong()
    {
        if (gameObject.GetComponent<AudioSource>().isPlaying == false)
            gameObject.GetComponent<AudioSource>().Play();
    }
    public void StopSong()
    {
        gameObject.GetComponent<AudioSource>().Stop();
    }
    public void ChangeSaturation()
    {
        //if(hasteacherended  && !prueba)
        //{
        //    prueba = true;
        //    ChangeFixedSaturation(-100.0f);
        //}

        int battleCount = CountBattlesWon();

        if (colorGrading != null && onCorridor)
        {
            // Determine the new saturation value based on the number of battles won
            //float newSaturation = 0f;
            
            if(battleCount <= 0)
            {
                if(newSaturation > -90)
                {
                    newSaturation -= Time.deltaTime * 150;
                }
                //newSaturation = -100f;
            }

            if (battleCount >= 1)
            {
                if (newSaturation < -60)
                {
                    newSaturation += Time.deltaTime * 150;
                }
                //newSaturation = -60f;
            }

            if (battleCount >= 2)
            {
                if (newSaturation < -30f)
                {
                    newSaturation += Time.deltaTime * 150;
                }
                //newSaturation = -30f;
            }

            if (battleCount >= 3)
            {
                if(newSaturation < 0.0f)
                {
                    newSaturation += Time.deltaTime * 150;
                }
                //newSaturation = 0f;
            }

            // Set the new saturation value
            colorGrading.saturation.value = newSaturation;
            Debug.Log("New saturation: " + newSaturation);
        }

    }

    public void ChangeFixedSaturation(float value)
    {
        if (colorGrading != null)
        {
            colorGrading.saturation.value = value;
        }
    }

    private int CountBattlesWon()
    {
        int battleCount = 0;

        if (battle1win)
        {
            battleCount++;
        }

        if (battle2win)
        {
            battleCount++;
        }

        if (battle3win)
        {
            battleCount++;
        }

        return battleCount;
    }

    public void RestartVariables()
    {
        hasteacherended = false;
        onCorridor = false;

        battle1enter = false;
        battle1win = false;
        battle2enter = false;
        battle2win = false;
        battle3enter = false;
        battle3win = false;

        battle1lost = false;
        battle2lost = false;
        battle3lost = false;

        isCutsceneOn = true;
        doorcloser = false;
        hascinematicended = false;
        hasheadset = false;

        prueba = false;
}
    

}
