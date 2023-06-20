using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScriptTeacher : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private bool start;
    private int index;
    private float time;
    public static bool useless;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        start = true;
        useless = false;
        time = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(start==true && TeacherScene.teacherscene == true)
        {
            if (GameManager.Instance.hasteacherended == false)
            {
                StartDialogue();
                start = false;
            }
        }
        if (time > 2.5)
        {
            if (textComponent.text == lines[index])
            {

                NextLine();

            }
        }
        if(time >= 0)
        {
            time += Time.deltaTime;
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
        time = 0;
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
            useless = true;
            Invoke(nameof(setactivefalse), 3);
        }
    }
    void setactivefalse()
    {
        gameObject.SetActive(false);
        SadSceneEnter.startsad = true;
    }
}