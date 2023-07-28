using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


/// Take drag-and-drop the panels to show the skills
///  and pass them to the singleton instance of LevelVariables
public class InitSkillPanels : MonoBehaviour
{
    public TextMeshProUGUI BodyPanelText, SoulPanelText, MindPanelText;

    public int InitialBodySkills, InitialSoulSkills, InitialMindSkills;

    // Start is called before the first frame update
    void Start()
    {
        LevelVariables.Instance().BodyPanelText = BodyPanelText;
        LevelVariables.Instance().SoulPanelText = SoulPanelText;
        LevelVariables.Instance().MindPanelText = MindPanelText;

        LevelVariables.Instance().bodyScore = InitialBodySkills;
        LevelVariables.Instance().soulScore = InitialSoulSkills;
        LevelVariables.Instance().mindScore = InitialMindSkills;


        LevelVariables.Instance().UpdateSkillPanels();
    }

}
