using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScriptGroup5 : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float lineDelay; // Tiempo de espera antes de pasar a la siguiente línea
    private int index;
    public static bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.battle3enter && start == false)
        {
            StartDialogue();
            start = true;

        }

        if (textComponent.text == lines[index])
        {
            StartCoroutine(NextLineWithDelay());
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator NextLineWithDelay()
    {
        yield return new WaitForSeconds(lineDelay);

        NextLine();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            Invoke(nameof(SetActiveFalse), 1);
        }
    }

    void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
