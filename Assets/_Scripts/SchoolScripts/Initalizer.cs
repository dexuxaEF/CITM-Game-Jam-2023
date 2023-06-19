using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initalizer : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public Animator corridor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.hasteacherended == true)
        {
            corridor.SetBool("classToCorridor", true);
        }
        if(GameManager.battle1win == true)
        {
            _rigidbody.position = GameManager.schoolpos;
        }

    }
}
