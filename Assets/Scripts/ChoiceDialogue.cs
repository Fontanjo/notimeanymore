using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
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
            gameObject.SetActive(false);

            AllowActions();
        }
    }

    public void NewDialogue(string[] newLines)
    {
        // Update lines
        lines = newLines;
        
        // Clear text
        textComponent.text = string.Empty;

        // Reactivate object
        gameObject.SetActive(true);

        // Start
        StartDialogue();
    }

    // Unlock actions that were locked during dialogue
    private void AllowActions()
    {
        // Allow movement
        //LevelVariables.Instance().AllowMovement();

        // Zoom out (continue game)
        cameraController.ZoomOut();
        
    }

}
