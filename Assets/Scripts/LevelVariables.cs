using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVariables
{
    public int soulScore = 3;
    public int bodyScore = 3;
    public int mindScore = 3;

    public Dictionary<string, int> achievementsDict = new Dictionary<string, int>();
    public Dictionary<string, int> objectDict = new Dictionary<string, int>();
    public Dictionary<string, string> questStagesDict = new Dictionary<string, string>();


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
        CheckDeath();
    }

    public void RemoveBody(int amount)
    {
        bodyScore -= amount;
        Debug.Log("Removing " + amount + " body");
        CheckDeath();
    }

    public void RemoveMind(int amount)
    {
        mindScore -= amount;
        Debug.Log("Removing " + amount + " mind");
        CheckDeath();
    }



    public void CheckDeath()
    {
       // If any soul/body/mind is <= 0, end scene
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
