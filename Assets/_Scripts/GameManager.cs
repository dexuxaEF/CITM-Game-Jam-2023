using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

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
    public bool isCutsceneOn = true;
    public bool doorcloser = false;
    public bool hascinematicended = false;
    public bool hasheadset = false;

    bool prueba = false;

    public PostProcessProfile postProcessingProfile;
    private ColorGrading colorGrading;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Combat1Scene")
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
        if (SceneManager.GetActiveScene().name == "School")
        {
            if(gameObject.GetComponent<AudioSource>().isPlaying == false)
            gameObject.GetComponent<AudioSource>().Play();
        }


        // Post process when battle is won
        // Changes the saturation :|
        ChangeSaturation();
    }

    public void ChangeSaturation()
    {
        if(hasteacherended  && !prueba)
        {
            prueba = true;
            ChangeFixedSaturation(-100.0f);
        }

        int battleCount = CountBattlesWon();

        if (colorGrading != null && onCorridor)
        {
            // Determine the new saturation value based on the number of battles won
            float newSaturation = 0f;

            if(battleCount <= 0)
            {
                newSaturation = -100f;
            }

            if (battleCount >= 1)
            {
                newSaturation = -60f;
            }

            if (battleCount >= 2)
            {
                newSaturation = -30f;
            }

            if (battleCount >= 3)
            {
                newSaturation = 0f;
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


}
