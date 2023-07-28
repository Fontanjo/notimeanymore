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
    private bool endedFinal = false;

    private Dictionary<string, Dictionary<string, string>> tileDialogueDict;

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
        if(Input.GetMouseButtonDown(0))
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
                if (!endedFinal)
                {
                  //StopAllCoroutines();  // Do not allow (for now)
                  //textComponent.text = lines[index];
                  //endedFinal = true;
                  Debug.Log("Update 1");
                }
                else
                {
                  //StopAllCoroutines();
                  Debug.Log("Update 2");

                  // Stop coroutines
                  StopAllCoroutines();
                  // Close dialogue
                  gameObject.SetActive(false);
                  // Allow movement
                  cameraController.ZoomOut();
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
        endedFinal = false;
        writingFinal = false;
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
            index = 0;
            writingFinal = true;
            //gameObject.SetActive(false);
            StopAllCoroutines();
            AllowActions();
        }
    }

    public void NewDialogue(Dictionary<string, Dictionary<string, string>> dialogueDict)
    {
        // Save reference to dict
        tileDialogueDict = dialogueDict;

        index = 0;
        endedFinal = false;
        writingFinal = false;

        string line = dialogueDict["dialogue"]["text"];

        c1text.text = dialogueDict["choice1"]["text"];
        c2text.text = dialogueDict["choice2"]["text"];
        c3text.text = dialogueDict["choice3"]["text"];


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


    private UnityAction choice1Action, choice2Action, choice3Action, m_MyFirstAction, endAction;

    // Unlock actions that were locked during dialogue
    private void AllowActions()
    {
        Debug.Log("Allow action");
        // Show choices
        ShowOptions();

        Button c1textButton = c1text.gameObject.transform.parent.GetComponent<Button>();
        Button c2textButton = c2text.gameObject.transform.parent.GetComponent<Button>();
        Button c3textButton = c3text.gameObject.transform.parent.GetComponent<Button>();

        //////////////////////////////////////////// TODO ////////////////////////////////////////////
        // Check for each choice if required_quest_stage >= ForestQuest0
        // If empty, ok
        // If DIFFERENT, block button (not only if smaller, but also if bigger)

        string c1requirement = tileDialogueDict["choice1"]["requiredQuestStage"];
        string c2requirement = tileDialogueDict["choice2"]["requiredQuestStage"];
        string c3requirement = tileDialogueDict["choice3"]["requiredQuestStage"];


        Color blackColor = new Color(0, 0, 0, 1);
        Color whiteColor = new Color(1, 1, 1, 1);
        if (!string.IsNullOrWhiteSpace(c1requirement))
        {
            Debug.Log("C1 requirement: "+ c1requirement);

            if (LevelVariables.Instance().MeetQuestStage(c1requirement))
            {
                Debug.Log("Requirement met!");

                c1textButton.enabled = true;
                c1textButton.GetComponent<Image>().color = whiteColor;
            }
            else
            {
                Debug.Log("Requirement not met");

                c1textButton.enabled = false;
                c1textButton.GetComponent<Image>().color = blackColor;
            }
        }

        if (!string.IsNullOrWhiteSpace(c2requirement))
        {
            Debug.Log("C2 requirement: "+ c2requirement);

            if (LevelVariables.Instance().MeetQuestStage(c2requirement))
            {
                Debug.Log("Requirement met!");

                c2textButton.enabled = true;
                c2textButton.GetComponent<Image>().color = whiteColor;
            }
            else
            {
                Debug.Log("Requirement not met");

                c2textButton.enabled = false;
                c2textButton.GetComponent<Image>().color = blackColor;
            }
        }

        if (!string.IsNullOrWhiteSpace(c3requirement))
        {
            Debug.Log("C3 requirement: "+ c3requirement);

            if (LevelVariables.Instance().MeetQuestStage(c3requirement))
            {
                Debug.Log("Requirement met!");

                c3textButton.enabled = true;
                c3textButton.GetComponent<Image>().color = whiteColor;
            }
            else
            {
                Debug.Log("Requirement not met");

                c3textButton.enabled = false;
                c3textButton.GetComponent<Image>().color = blackColor;
            }
        }

        choice1Action += Choice1;
        choice2Action += Choice2;
        choice3Action += Choice3;

        c1textButton.onClick.AddListener(choice1Action);
        c2textButton.onClick.AddListener(choice2Action);
        c3textButton.onClick.AddListener(choice3Action);
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

    private void Choice1()
    {
        ChoiceGeneral("choice1");
    }

    private void Choice2()
    {
        ChoiceGeneral("choice2");
    }

    private void Choice3()
    {
        ChoiceGeneral("choice3");
    }

    private void ChoiceGeneral(string choiceCode)
    {
        Dictionary<string, string> choiceDict = tileDialogueDict[choiceCode];

        //////////////////////////////////////////// Done ////////////////////////////////////////////
        // Get difficulty (chance of success)
        // low  -> random > 33 --> badOutcome
        // medium -> 50
        // high -> 75
        //

        string difficulty = choiceDict["difficulty"];
        int chances;
        switch(difficulty)
        {
            case "low":
                chances = 33;
                break;
            case "medium":
                chances = 50;
                break;
            case "high":
                chances = 75;
                break;
            default:
                chances = 100;
                break;
        }


        //////////////////////////////////////////// Done ////////////////////////////////////////////
        // Get outcome

        int dice = Random.Range(0,101);
        bool succeeded = dice < chances;


        //////////////////////////////////////////// Done ////////////////////////////////////////////
        // Get skill type
        // ame
        // corps
        // esprit
        //
        // Remove skill amount
        //   (aws 1)

        string skillType = choiceDict["skillType"];
        int skillAmount = int.Parse(choiceDict["skillAmount"]);

        switch(skillType)
        {
            case "ame":
                LevelVariables.Instance().RemoveSoul(skillAmount);
                break;
            case "corps":
                LevelVariables.Instance().RemoveBody(skillAmount);
                break;
            case "esprit":
                LevelVariables.Instance().RemoveMind(skillAmount);
                break;
            default:
                Debug.Log("Skill type not recognized");
                break;
        }



        //////////////////////////////////////////// Done ////////////////////////////////////////////
        // GainAchievement
        // Both possible with good and bad outcome
        // INCREASE achievement count
        string gainedAchievement;
        if (succeeded)
        {
            gainedAchievement = choiceDict["goodGainedAchievement"];
        }
        else
        {
            gainedAchievement = choiceDict["badGainedAchievement"];
        }
        // Save achievement
        if (!string.IsNullOrWhiteSpace(gainedAchievement))
        {
            LevelVariables.Instance().AddAchievement(gainedAchievement);
        }

        //////////////////////////////////////////// Done ////////////////////////////////////////////
        // Set quest stage
        // ForestQuest 0/1/2
        // DesertQuest 0/1/2/..(?)
        // MountQuest 0/1/2/..(?)
        // MarshQuest 0/1/2/..(?)
        string newQuestStage;
        if (succeeded)
        {
            newQuestStage = choiceDict["goodSetQuestStage"];
        }
        else
        {
            newQuestStage = choiceDict["badSetQuestStage"];
        }
        // Save achievement
        if (!string.IsNullOrWhiteSpace(newQuestStage))
        {
            LevelVariables.Instance().SetQuestStage(newQuestStage);
        }

        //////////////////////////////////////////// Done ////////////////////////////////////////////
        // Gain/Lose object
        // INCREASE object count
        // If lose, lose random object
        string gainedObject;
        string lostObject;
        if (succeeded) // Don't really need this check but ok
        {
            gainedObject = choiceDict["goodGainedObject"];
            if (!string.IsNullOrWhiteSpace(gainedObject))
            {
                LevelVariables.Instance().AddObject(gainedObject);
            }
        }
        else
        {
            lostObject = choiceDict["badLostObject"];
            if (!string.IsNullOrWhiteSpace(lostObject))
            {
                LevelVariables.Instance().RemoveObject();
            }
        }



        //////////////////////////////////////////// Done ////////////////////////////////////////////
        // Get skill price and remove
        //   type
        //   amount
        // Empty for no
        string additionalSkillType;
        string additionalSkillAmount;
        if (succeeded)
        {
            additionalSkillType = choiceDict["goodskillType"];
            additionalSkillAmount = choiceDict["goodskillAmount"];
        }
        else
        {
            additionalSkillType = choiceDict["badskillType"];
            additionalSkillAmount = choiceDict["badskillAmount"];
        }

        if (!string.IsNullOrWhiteSpace(additionalSkillType) && !string.IsNullOrWhiteSpace(additionalSkillAmount))
        {
            int additionalSkillAmountInt = int.Parse(additionalSkillAmount);

            switch(additionalSkillType)
            {
                case "ame":
                    LevelVariables.Instance().RemoveSoul(additionalSkillAmountInt);
                    break;
                case "corps":
                    LevelVariables.Instance().RemoveBody(additionalSkillAmountInt);
                    break;
                case "esprit":
                    LevelVariables.Instance().RemoveMind(additionalSkillAmountInt);
                    break;
                default:
                    Debug.Log("Skill type not recognized");
                    break;
            }
        }



        string finalMessage;

        // Write outcome message
        if (succeeded)
        {
            finalMessage = tileDialogueDict["choice1"]["goodDialogueText"];
        }
        else
        {
            finalMessage = tileDialogueDict["choice1"]["badDialogueText"];
        }

        string[] newLine = {finalMessage};
        lines = newLine;

        HideOptions();

        textComponent.text = string.Empty;

        Debug.Log("Write final textttttttttttttttttttttttt.............");
        Debug.Log("Write final textttttttttttttttttttttttt.............");
        Debug.Log("Write final textttttttttttttttttttttttt.............");
        writingFinal = true;
        foreach (string l in lines) {
            Debug.Log(l);
        }

        StartCoroutine(TypeFinalLine());
    }


    private void LoadEndScene()
    {
        Debug.Log("End action");
        GlobalVariables.Set("currentLevelIndex", 1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
    }


    IEnumerator TypeFinalLine()
    {
        // Dialogue
        // foreach (char c in lines[index].ToCharArray())
        // {
        //     textComponent.text += c;
        //     yield return new WaitForSeconds(textSpeed);
        // }

        //////////////// Hard fix of the problem ////////
        //////////////// BUG ////////////////
        // When iterating, from the second tile it will write multiple time each char
        // For now just put entire text directly
        textComponent.text = lines[0];
        endedFinal = true;
        yield return true;
    }

}
