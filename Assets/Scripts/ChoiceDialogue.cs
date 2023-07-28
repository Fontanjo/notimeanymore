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
            //gameObject.SetActive(false);

            AllowActions();
        }
    }

    public void NewDialogue(string[] newLines, string[] choices)
    {
        // Update lines
        lines = newLines;

        // Should check if the length is 3
        c1text.text = choices[0];
        c2text.text = choices[1];
        c3text.text = choices[2];

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
        Debug.Log("Button clicked!");
        gameObject.SetActive(false);
        cameraController.ZoomOut();
    }

    private void LoadEndScene()
    {
        GlobalVariables.Set("currentLevelIndex", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }

}
