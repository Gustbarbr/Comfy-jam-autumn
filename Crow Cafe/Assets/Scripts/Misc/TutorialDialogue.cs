using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialDialogue : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string[] lines;
    public float textSpeed;

    public bool endTutorial = false;

    private int index = 0;

    public string tutorialKey = "Tutorial_Default";

    private void Start()
    {
        if (PlayerPrefs.GetInt(tutorialKey, 0) == 1)
        {
            gameObject.SetActive(false);
            return;
        }

        text.text = string.Empty;
        StartDialogue();
        PlayerPrefs.SetInt(tutorialKey, 1);
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(text.text == lines[index])
                NextLine();

            else
            {
                StopAllCoroutines();
                text.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed); 
        }
    }

    public void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        }

        else
        {
            gameObject.SetActive(false);
            endTutorial = true;
        }
    }
}
