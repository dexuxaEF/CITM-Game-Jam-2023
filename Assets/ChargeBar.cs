using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Slider slider;
    public void SetCharge(float charge) 
    {

        slider.value = charge;
    }

    public void SetMaxCharge(float maxcharge)
    {

        slider.maxValue = maxcharge;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
