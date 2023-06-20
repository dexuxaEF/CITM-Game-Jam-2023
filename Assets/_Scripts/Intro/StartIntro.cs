using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIntro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Invoke(nameof(StartDialogue), 7);

    }
    void StartDialogue()
    {
        if (Dialogue.next == false)
        {
            Dialogue.startdialogue = true;
        }
    }
}
