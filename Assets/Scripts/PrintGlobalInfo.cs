using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrintGlobalInfo : MonoBehaviour
{

    [Space(20)]
    public TextMeshProUGUI MushroomKillTxt;
    public TextMeshProUGUI InsectKillTxt, GnomeKillTxt, HarpyKillTxt;

    [Space(20)]
    public TextMeshProUGUI ForestBossTxt;
    public TextMeshProUGUI DesertBossTxt, MarshBossTxt, MountBossTxt;

    [Space(20)]
    public TextMeshProUGUI RelicCountTxt;
    public TextMeshProUGUI ScrollCountTxt, OfferingCountTxt, GemCountTxt;

    [Space(20)]
    public GameObject GetHighBlock;
    public GameObject PartyCrasherBlock, SunBathingBlock, SnowBoardingBlock;


    [Space(20)]
    public GameObject ForestQuestBlock;
    public GameObject MarshQuestBlock, DesertQuestBlock, MountQuestBlock;


    [Space(30)]
    public TextMeshProUGUI LeftPageIntro;
    public TextMeshProUGUI RightPageIntro, ForestIntro, DesertIntro, MarshIntro, MountIntro;

    [Space(30)]
    public FinalXMLReader xmlReader;

    // Minimum amount achieved to show the text and the icon
    private int minimumToShow = 0; // 0 correct, -1 to show
    private string targetQuestStage = "3"; // "3" correct, null to show


    // Start is called before the first frame update
    void Start()
    {
        SetTexts();

        SetKills();
        SetBosses();
        SetObjects();

        SetAchievements();
        SetStages();
    }


    // Set the texts that do not vary, loading them from the XML file
    private void SetTexts()
    {
        Dictionary<string, string> dict = xmlReader.dataDict;
        


        LeftPageIntro.text = dict["leftIntro"];
        
        RightPageIntro.text = dict["rigthIntro"];



        ForestIntro.text = dict["leftForest"];

        DesertIntro.text = dict["leftMarsh"];

        MarshIntro.text = dict["leftDesert"];

        MountIntro.text = dict["leftMount"];



        TextMeshProUGUI getHighTxt = GetHighBlock.GetComponentInChildren<TextMeshProUGUI>();
        getHighTxt.text = dict["getHigh"];

        TextMeshProUGUI partyCrasherTxt = PartyCrasherBlock.GetComponentInChildren<TextMeshProUGUI>();
        partyCrasherTxt.text = dict["partyCrasher"];

        TextMeshProUGUI sunBathingTxt = SunBathingBlock.GetComponentInChildren<TextMeshProUGUI>();
        sunBathingTxt.text = dict["sunBathing"];

        TextMeshProUGUI snowBoardingTxt = SnowBoardingBlock.GetComponentInChildren<TextMeshProUGUI>();
        snowBoardingTxt.text = dict["snowBoarding"];



        TextMeshProUGUI ForesQuestTxt = ForestQuestBlock.GetComponentInChildren<TextMeshProUGUI>();
        ForesQuestTxt.text = dict["ForestQuest"];

        TextMeshProUGUI MarshQuestTxt = MarshQuestBlock.GetComponentInChildren<TextMeshProUGUI>();
        MarshQuestTxt.text = dict["MarshQuest"];

        TextMeshProUGUI DesertQuestTxt = DesertQuestBlock.GetComponentInChildren<TextMeshProUGUI>();
        DesertQuestTxt.text = dict["DesertQuest"];

        TextMeshProUGUI MountQuestTxt = MountQuestBlock.GetComponentInChildren<TextMeshProUGUI>();
        MountQuestTxt.text = dict["MountQuest"];
    }


    private void SetKills()
    {
        int MushroomKill = GlobalVariables.Get<int>("MushroomKill");
        MushroomKillTxt.text = "" + MushroomKill;
        if (MushroomKill > minimumToShow)
        {
            MushroomKillTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            MushroomKillTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int InsectKill = GlobalVariables.Get<int>("InsectKill");
        InsectKillTxt.text = "" + InsectKill;
        if (InsectKill > minimumToShow)
        {
            InsectKillTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            InsectKillTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int GnomeKill = GlobalVariables.Get<int>("GnomeKill");
        GnomeKillTxt.text = "" + GnomeKill;
        if (GnomeKill > minimumToShow)
        {
            GnomeKillTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            GnomeKillTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int HarpyKill = GlobalVariables.Get<int>("HarpyKill");
        HarpyKillTxt.text = "" + HarpyKill;
        if (HarpyKill > minimumToShow)
        {
            HarpyKillTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            HarpyKillTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }


    private void SetBosses()
    {
        int ForestBoss = GlobalVariables.Get<int>("ForestBoss");
        ForestBossTxt.text = "" + ForestBoss;
        if (ForestBoss > minimumToShow)
        {
            ForestBossTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            ForestBossTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int DesertBoss = GlobalVariables.Get<int>("DesertBoss");
        DesertBossTxt.text = "" + DesertBoss;
        if (DesertBoss > minimumToShow)
        {
            DesertBossTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            DesertBossTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int MarshBoss = GlobalVariables.Get<int>("MarshBoss");
        MarshBossTxt.text = "" + MarshBoss;
        if (MarshBoss > minimumToShow)
        {
            MarshBossTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            MarshBossTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int MountBoss = GlobalVariables.Get<int>("MountBoss");
        MountBossTxt.text = "" + MountBoss;
        if (MountBoss > minimumToShow)
        {
            MountBossTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            MountBossTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void SetObjects()
    {
        int RelicCount = GlobalVariables.Get<int>("RelicCount");
        RelicCountTxt.text = "" + RelicCount;
        if (RelicCount > minimumToShow)
        {
            RelicCountTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            RelicCountTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int ScrollCount = GlobalVariables.Get<int>("ScrollCount");
        ScrollCountTxt.text = "" + ScrollCount;
        if (ScrollCount > minimumToShow)
        {
            ScrollCountTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            ScrollCountTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int OfferingCount = GlobalVariables.Get<int>("OfferingCount");
        OfferingCountTxt.text = "" + OfferingCount;
        if (OfferingCount > minimumToShow)
        {
            OfferingCountTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            OfferingCountTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }

        int GemCount = GlobalVariables.Get<int>("GemCount");
        GemCountTxt.text = "" + GemCount;
        if (GemCount > minimumToShow)
        {
            GemCountTxt.gameObject.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            GemCountTxt.gameObject.transform.parent.gameObject.SetActive(false);
        }
    }

    private void SetAchievements()
    {
        int GetHigh = GlobalVariables.Get<int>("GetHigh");
        if (GetHigh > minimumToShow)
        {
            GetHighBlock.gameObject.SetActive(true);
        }
        else
        {
            GetHighBlock.gameObject.SetActive(false);
        }

        int PartyCrasher = GlobalVariables.Get<int>("PartyCrasher");
        if (PartyCrasher > minimumToShow)
        {
            PartyCrasherBlock.gameObject.SetActive(true);
        }
        else
        {
            PartyCrasherBlock.gameObject.SetActive(false);
        }

        int SunBathing = GlobalVariables.Get<int>("SunBathing");
        if (SunBathing > minimumToShow)
        {
            SunBathingBlock.gameObject.SetActive(true);
        }
        else
        {
            SunBathingBlock.gameObject.SetActive(false);
        }

        int SnowBoarding = GlobalVariables.Get<int>("SnowBoarding");
        if (SnowBoarding > minimumToShow)
        {
            SnowBoardingBlock.gameObject.SetActive(true);
        }
        else
        {
            SnowBoardingBlock.gameObject.SetActive(false);
        }
    }


    private void SetStages()
    {
        string ForestQuest = GlobalVariables.Get<string>("ForestQuest");
        if (ForestQuest == targetQuestStage)
        {
            ForestQuestBlock.gameObject.SetActive(true);
        }
        else
        {
            ForestQuestBlock.gameObject.SetActive(false);
        }

        string MarshQuest = GlobalVariables.Get<string>("MarshQuest");
        if (MarshQuest == targetQuestStage)
        {
            MarshQuestBlock.gameObject.SetActive(true);
        }
        else
        {
            MarshQuestBlock.gameObject.SetActive(false);
        }

        string DesertQuest = GlobalVariables.Get<string>("DesertQuest");
        if (DesertQuest == targetQuestStage)
        {
            DesertQuestBlock.gameObject.SetActive(true);
        }
        else
        {
            DesertQuestBlock.gameObject.SetActive(false);
        }

        string MountQuest = GlobalVariables.Get<string>("MountQuest");
        if (MountQuest == targetQuestStage)
        {
            MountQuestBlock.gameObject.SetActive(true);
        }
        else
        {
            MountQuestBlock.gameObject.SetActive(false);
        }
    }
}
