using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelVariables
{
    public int soulScore = 3;
    public int bodyScore = 3;
    public int mindScore = 3;

    public Dictionary<string, int> achievementsDict = new Dictionary<string, int>();
    public Dictionary<string, int> objectDict = new Dictionary<string, int>();
    public Dictionary<string, string> questStagesDict = new Dictionary<string, string>();

    public TextMeshProUGUI BodyPanelText, SoulPanelText, MindPanelText;


    // Singleton, allow only 1 instance of the class
    public static LevelVariables Instance()
    {
        if (_Instance == null)
            _Instance = new LevelVariables();
        return _Instance;
    }

    private static LevelVariables _Instance;
    private bool canMove = false;
    private bool canSelectEvent = false;



    public bool CanMove()
    {
        return canMove;
    }

    public void AllowMovement()
    {
        Debug.Log("Movement unlocked");
        canMove = true;
    }

    public void BlockMovement()
    {
        Debug.Log("Movement blocked");
        canMove = false;
    }


    public bool CanSelectEvent()
    {
        return canSelectEvent;
    }

    public void AllowSelectEvent()
    {
        Debug.Log("Event selection unlocked");
        canSelectEvent = true;
    }

    public void BlockSelectEvent()
    {
        Debug.Log("Event selection blocked");
        canSelectEvent = false;
    }


    public void RemoveSoul(int amount)
    {
        soulScore -= amount;
        Debug.Log("Removing " + amount + " soul");

        UpdateSkillPanels();

        CheckDeath();
    }

    public void RemoveBody(int amount)
    {
        bodyScore -= amount;
        Debug.Log("Removing " + amount + " body");

        UpdateSkillPanels();

        CheckDeath();
    }

    public void RemoveMind(int amount)
    {
        mindScore -= amount;
        Debug.Log("Removing " + amount + " mind");

        UpdateSkillPanels();

        CheckDeath();
    }

    public void UpdateSkillPanels()
    {
      // Update UI panel
      MindPanelText.text = "" + mindScore;

      // Update UI panel
      BodyPanelText.text = "" + bodyScore;

      // Update UI panel
      SoulPanelText.text = "" + soulScore;
    }


    public void CheckDeath()
    {

        bool dead = false;

        if (soulScore <= 0)
        {
            dead = true;
            Debug.Log("Dead: soul value " + soulScore);
        }

        if (bodyScore <= 0)
        {
            dead = true;
            Debug.Log("Dead: body value " + bodyScore);
        }

        if (mindScore <= 0)
        {
            dead = true;
            Debug.Log("Dead: mind value " + mindScore);
        }

        if (dead)
        {
            //////////////////////////////////////////// TODO ////////////////////////////////////////////

            // Save achievements

            int musKill;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("MushroomKill", out musKill);
            GlobalVariables.Set("MushroomKill", musKill);
            // Reset
            // achievementsDict["MushroomKill"] = 0;


            int InsectKill;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("InsectKill", out InsectKill);
            GlobalVariables.Set("InsectKill", InsectKill);
            // Reset
            // achievementsDict["InsectKill"] = 0;


            int GnomeKill;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("GnomeKill", out GnomeKill);
            GlobalVariables.Set("GnomeKill", GnomeKill);
            // Reset
            // achievementsDict["GnomeKill"] = 0;


            int HarpyKill;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("HarpyKill", out HarpyKill);
            GlobalVariables.Set("HarpyKill", HarpyKill);
            // Reset
            // achievementsDict["HarpyKill"] = 0;



            // Save objects

            int RelicCount;
            // currentCount will be zero if the key id doesn't exist
            objectDict.TryGetValue("RelicCount", out RelicCount);
            GlobalVariables.Set("RelicCount", RelicCount);
            // Reset
            // objectDict["RelicCount"] = 0;

            int ScrollCount;
            // currentCount will be zero if the key id doesn't exist
            objectDict.TryGetValue("ScrollCount", out ScrollCount);
            GlobalVariables.Set("ScrollCount", ScrollCount);
            // Reset
            // objectDict["ScrollCount"] = 0;

            int OfferingCount;
            // currentCount will be zero if the key id doesn't exist
            objectDict.TryGetValue("OfferingCount", out OfferingCount);
            GlobalVariables.Set("OfferingCount", OfferingCount);
            // Reset
            // objectDict["OfferingCount"] = 0;

            int GemCount;
            // currentCount will be zero if the key id doesn't exist
            objectDict.TryGetValue("GemCount", out GemCount);
            GlobalVariables.Set("GemCount", GemCount);
            // Reset
            // objectDict["GemCount"] = 0;


            // Save extra achievements

            int GetHigh;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("GetHigh", out GetHigh);
            GlobalVariables.Set("GetHigh", GetHigh);
            // Reset
            // achievementsDict["GetHigh"] = 0;


            int PartyCrasher;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("PartyCrasher", out PartyCrasher);
            GlobalVariables.Set("PartyCrasher", PartyCrasher);
            // Reset
            // achievementsDict["PartyCrasher"] = 0;


            int SunBathing;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("SunBathing", out SunBathing);
            GlobalVariables.Set("SunBathing", SunBathing);
            // Reset
            // achievementsDict["SunBathing"] = 0;


            int SnowBoarding;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("SnowBoarding", out SnowBoarding);
            GlobalVariables.Set("SnowBoarding", SnowBoarding);
            // Reset
            // achievementsDict["SnowBoarding"] = 0;



            // Save boss achievements

            int ForestBoss;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("ForestBoss", out ForestBoss);
            GlobalVariables.Set("ForestBoss", ForestBoss);
            // Reset
            // achievementsDict["ForestBoss"] = 0;


            int DesertBoss;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("DesertBoss", out DesertBoss);
            GlobalVariables.Set("DesertBoss", DesertBoss);
            // Reset
            // achievementsDict["DesertBoss"] = 0;


            int MarshBoss;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("MarshBoss", out MarshBoss);
            GlobalVariables.Set("MarshBoss", MarshBoss);
            // Reset
            // achievementsDict["MarshBoss"] = 0;


            int MountBoss;
            // currentCount will be zero if the key id doesn't exist
            achievementsDict.TryGetValue("MountBoss", out MountBoss);
            GlobalVariables.Set("MountBoss", MountBoss);
            // Reset
            // achievementsDict["MountBoss"] = 0;




            // Save all necessary infos
            Debug.Log("You died!######################################");
            UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
        }
    }

    public void ResetDict()
    {

    }




    public void AddAchievement(string name)
    {
        int currentCount;

        // currentCount will be zero if the key id doesn't exist
        achievementsDict.TryGetValue(name, out currentCount);

        // Update count
        achievementsDict[name] = currentCount + 1;
    }

    public void AddObject(string name)
    {
        int currentCount;

        // currentCount will be zero if the key id doesn't exist
        objectDict.TryGetValue(name, out currentCount);

        // Update count
        objectDict[name] = currentCount + 1;

        Debug.Log("Object " + name + " added!");
    }

    public void RemoveObject()
    {
        // Remove RANDOM object
        //////////////////////////////////////////// TODO ////////////////////////////////////////////
        Debug.Log("Random object lost..");
    }

    public void SetQuestStage(string questStage)
    {
        string key = GetQuestKey(questStage);
        string value = "" + questStage[questStage.Length - 1];

        questStagesDict[key] = value;

        Debug.Log("Quest stage for " + key + ": " + value);
    }

    public bool MeetQuestStage(string questStage)
    {
        string key = GetQuestKey(questStage);
        string value = "" + questStage[questStage.Length - 1];

        // Try to get current stage
        string currentStage;
        // currentCount will be zero if the key id doesn't exist
        questStagesDict.TryGetValue(key, out currentStage);

        // If no stage set, the it's at 0
        if (string.IsNullOrWhiteSpace(currentStage))
        {
            currentStage = "" + 0;
        }

        return (currentStage == value);
    }

    private string GetQuestKey(string questStageName)
    {
        // Probably one of the ugliest part of the code
        if (questStageName[0] == 'F')
        {
            return "ForesQuest";
        }

        if (questStageName[0] == 'D')
        {
            return "DesertQuest";
        }


        if (questStageName[0] == 'M')
        {
            if (questStageName[1] == 'o')
            {
                return "MountQuest";
            }

            if (questStageName[1] == 'a')
            {
                return "MarshQuest";
            }

            return "OtherMQuest";
        }

        return "OtherQuest";
    }
}
