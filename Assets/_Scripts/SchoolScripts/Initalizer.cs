using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initalizer : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public Animator corridor;
    Vector2 position;
    bool cantp;
    // Start is called before the first frame update
    void Start()
    {
        cantp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.hasteacherended == true)
        {
            corridor.SetBool("classToCorridor", true);
        }
        if(GameManager.Instance.battle1win == true && cantp == true)
        {
            if (GameManager.Instance.battle2win == false)
            {
                _rigidbody.position = new Vector2(10.446f, 17.06502f);
                cantp = false;
            }
        }
        if (GameManager.Instance.battle2win == true && cantp == true)
        {
            if (GameManager.Instance.battle3win == false)
            {
                _rigidbody.position = new Vector2(10.446f,24.56795f);
                cantp = false;
            }
        }
        if (GameManager.Instance.battle3win == true && cantp == true)
        {

            _rigidbody.position = new Vector2(10.446f, 31.58f);
            cantp = false;

        }
    }
}
