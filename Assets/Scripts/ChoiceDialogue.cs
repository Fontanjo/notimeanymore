using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ChoiceDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent, c1text, c2text, c3text;
    public string[] lines;
    public float textSpeed;


    private int index;
    private CameraController cameraController;
    private bool writingFinal = false;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;

        // Get camera controller
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();

        // Deactivate object
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // TODO allow for any input? Otherwise maybe tell use to click
        {
            if (!writingFinal)
            {
                if (textComponent.text == lines[index])
                {
                  NextLine();
                }
                else
                {
                  StopAllCoroutines();
                  textComponent.text = lines[index];
                }
            }
            else
            {
                if (textComponent.text == lines[index])
                {
                  //NextLine();
                  Debug.Log("Update 1");
                }
                else
                {
                  //StopAllCoroutines();
                  //textComponent.text = lines[index];
                  Debug.Log("Update 2");
                }
            }
        }
    }

    void StartDialogue()
    {
        Debug.Log("Start choice dialogue");
        // Block movement to until end of dialogue
        // Block select event until end of dialogue
        LevelVariables.Instance().BlockMovement();
        LevelVariables.Instance().BlockSelectEvent();

        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Dialogue
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length -1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            writingFinal = true;
            //gameObject.SetActive(false);
            StopAllCoroutines();
            AllowActions();
        }
    }

    public void NewDialogue(Dictionary<string, Dictionary<string, string>> dialogueDict)
    {
        string line = dialogueDict["dialogue"]["text"];

        c1text.text = dialogueDict["choice1"]["text"];
        c2text.text = dialogueDict["choice2"]["text"];
        c3text.text = dialogueDict["choice2"]["text"];

        string[] newLine = {line};
        lines = newLine;

        // Hide options panels
        HideOptions();


        // Clear text
        textComponent.text = string.Empty;

        // Reactivate object
        gameObject.SetActive(true);

        // Start
        StartDialogue();
    }


    private UnityAction m_MyFirstAction, endAction;

    // Unlock actions that were locked during dialogue
    private void AllowActions()
    {
        Debug.Log("Allow action");
        // Show choices
        ShowOptions();

        m_MyFirstAction += Leave;

        endAction += LoadEndScene;

        c1text.gameObject.transform.parent.GetComponent<Button>().onClick.AddListener(m_MyFirstAction);
        c2text.gameObject.transform.parent.GetComponent<Button>().onClick.AddListener(m_MyFirstAction);
        c3text.gameObject.transform.parent.GetComponent<Button>().onClick.AddListener(endAction);
    }

    private void HideOptions()
    {
        c1text.gameObject.transform.parent.gameObject.SetActive(false);
        c2text.gameObject.transform.parent.gameObject.SetActive(false);
        c3text.gameObject.transform.parent.gameObject.SetActive(false);
    }

    private void ShowOptions()
    {
        c1text.gameObject.transform.parent.gameObject.SetActive(true);
        c2text.gameObject.transform.parent.gameObject.SetActive(true);
        c3text.gameObject.transform.parent.gameObject.SetActive(true);
    }

    private void Leave()
    {
        Debug.Log("leaving");
        string finalMessage = "GoodBy";
        string[] newLine = {finalMessage};
        lines = newLine;

        textComponent.text = string.Empty;
        index = 0;

        StartCoroutine(TypeFinalLine());
        Debug.Log("Button clicked!");
    }

    private void LoadEndScene()
    {
        Debug.Log("End action");
        // GlobalVariables.Set("currentLevelIndex", 1);
        // UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }


    IEnumerator TypeFinalLine()
    {
        // Dialogue
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        //Debug.Log("end final line");
        //gameObject.SetActive(false);
        //cameraController.ZoomOut();
    }

}
