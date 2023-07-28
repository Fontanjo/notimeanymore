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
    public TextMeshProUGUI ScrollCountTxt, aa, bb;


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



        int RelicCount = GlobalVariables.Get<int>("RelicCount");
        RelicCountTxt.text = "RelicCount: " + RelicCount;

        int ScrollCount = GlobalVariables.Get<int>("ScrollCount");
        ScrollCountTxt.text = "ScrollCount: " + ScrollCount;

        // int relicCount = GlobalVariables.Get<int>("RelicCount");
        // relicCountTxt.text = "RelicCount: " + relicCount;
        //
        // int relicCount = GlobalVariables.Get<int>("RelicCount");
        // relicCountTxt.text = "RelicCount: " + relicCount;
    }
}
