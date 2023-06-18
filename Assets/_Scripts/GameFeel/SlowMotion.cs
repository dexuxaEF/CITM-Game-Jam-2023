using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    public float slowMotionTimescale;

    private float startTimescale;
    private float startFixedDeltaTime;

    void Start()
    {
        startTimescale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    //void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        StartCoroutine(DamageSlowMo(2.0f));
    //    }


    //}

    private void StartSlowMotion()
    {
        Time.timeScale = slowMotionTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime * slowMotionTimescale;
    }

    private void StopSlowMotion()
    {
        Time.timeScale = startTimescale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

    public void StartDamageSlowMo(float duration)
    {
        StartCoroutine(DamageSlowMo(duration));
    }

    private IEnumerator DamageSlowMo(float duration)
    {
        StartSlowMotion();
        yield return new WaitForSecondsRealtime(duration);
        StopSlowMotion();
    }
}