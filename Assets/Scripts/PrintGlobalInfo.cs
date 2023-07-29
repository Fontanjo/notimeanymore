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
    public TextMeshProUGUI RelicCountTxt;
    public TextMeshProUGUI ScrollCountTxt, OfferingCountTxt, GemCountTxt;

    [Space(20)]
    public TextMeshProUGUI GetHighTxt;
    public TextMeshProUGUI PartyCrasherTxt, SunBathingTxt, SnowBoardingTxt;


    // Start is called before the first frame update
    void Start()
    {
        int MushroomKill = GlobalVariables.Get<int>("MushroomKill");
        MushroomKillTxt.text = "MushroomKill: " + MushroomKill;

        int InsectKill = GlobalVariables.Get<int>("InsectKill");
        InsectKillTxt.text = "InsectKill: " + InsectKill;

        int GnomeKill = GlobalVariables.Get<int>("GnomeKill");
        GnomeKillTxt.text = "GnomeKill: " + GnomeKill;

        int HarpyKill = GlobalVariables.Get<int>("HarpyKill");
        HarpyKillTxt.text = "HarpyKill: " + HarpyKill;

        //




        int RelicCount = GlobalVariables.Get<int>("RelicCount");
        RelicCountTxt.text = "RelicCount: " + RelicCount;

        int ScrollCount = GlobalVariables.Get<int>("ScrollCount");
        ScrollCountTxt.text = "ScrollCount: " + ScrollCount;

        int OfferingCount = GlobalVariables.Get<int>("OfferingCount");
        OfferingCountTxt.text = "OfferingCount: " + OfferingCount;

        int GemCount = GlobalVariables.Get<int>("GemCount");
        GemCountTxt.text = "GemCount: " + GemCount;




        int GetHigh = GlobalVariables.Get<int>("GetHigh");
        GetHighTxt.text = "GetHigh: " + GetHigh;

        int PartyCrasher = GlobalVariables.Get<int>("PartyCrasher");
        PartyCrasherTxt.text = "PartyCrasher: " + PartyCrasher;

        int SunBathing = GlobalVariables.Get<int>("SunBathing");
        SunBathingTxt.text = "SunBathing: " + SunBathing;

        int SnowBoarding = GlobalVariables.Get<int>("SnowBoarding");
        SnowBoardingTxt.text = "SnowBoarding: " + SnowBoarding;
    }
}
