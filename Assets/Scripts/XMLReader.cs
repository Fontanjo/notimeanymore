using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class XMLReader : MonoBehaviour
{
    //public TextMesh textMesh; // Assign a TextMesh component in the Inspector to display the loaded text
    //public string xmlFilePath = "Assets/Resources/data.xml"; // Path to the XML file

    public TextAsset textAsset;

    // Start is called before the first frame update
    void Start()
    {
        LoadExternalText();
    }

    private void LoadExternalText()
    {
        // Load the XML file
        //TextAsset textAsset = Resources.Load<TextAsset>(xmlFilePath);
        if (textAsset == null)
        {
            Debug.LogError("XML file not found");
            return;
        }

        // Parse the XML content
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);


        // Find all nodes named 'env'
        XmlNodeList envNodes = xmlDoc.SelectNodes("env");

        Dictionary<string, Dictionary<string, Dictionary<string, string>>> dataDict = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        if (envNodes.Count > 0)
        {
            // Iterate over each 'env' node
            foreach (XmlNode envNode in envNodes)
            {
                // Dict to save all the infos
                // C# does not allow for dict of different types (without casting)
                //   Update: actually possible, see GlobalVariables script. Basically create a dict of <string, object> and set type when removing (dict.Get<int>(keyOfIntElement))
                //   Will not change it now
                // Structure will be:
                //    dialogue -> text -> Text for first dialogue
                //    choice1  -> text -> Text for choice 1
                //             -> difficulty -> Difficulty for choice 1
                //             -> ....
                //    choice2  -> text -> Text for choice 2
                //             -> difficulty -> Difficulty for choice 2
                //             -> ....
                //    choice3  -> text -> Text for choice 3
                //             -> difficulty -> Difficulty for choice 3
                //             -> ....
                Dictionary<string, Dictionary<string, string>> envDict = new Dictionary<string, Dictionary<string, string>>();


                // Load name/id
                string envName = envNode.SelectSingleNode("envName").InnerText.Trim(); // Trim to remove start and end white spaces
                //Debug.Log("env name: " + envName);

                // Load dialogue text
                string dialogueText = envNode.SelectSingleNode("dialogue/text").InnerText.Trim();
                //Debug.Log(dialogueText);

                // Put text into dict since c# does not allow to mix dicts and strings as dict values
                Dictionary<string, string> diologueDict = new Dictionary<string, string>();
                diologueDict.Add("text", dialogueText);
                envDict.Add("dialogue", diologueDict);
                

                // Load choice 1 node
                XmlNode c1node = envNode.SelectSingleNode("choice1");
                Dictionary<string, string> c1Dict = LoadChoice(c1node);
                envDict.Add("choice1", c1Dict);

                XmlNode c2node = envNode.SelectSingleNode("choice2");
                Dictionary<string, string> c2Dict = LoadChoice(c2node);
                envDict.Add("choice2", c2Dict);

                XmlNode c3node = envNode.SelectSingleNode("choice3");
                Dictionary<string, string> c3Dict = LoadChoice(c3node);
                envDict.Add("choice3", c3Dict);



                // Save to outer dict
                dataDict.Add(envName, envDict);
            }

            // Pass to level generator (which should store all the info necessary for the generation of the level)
            LevelGenerator.SetDialoguesDataDict(dataDict);
        }
        else
        {
            Debug.LogWarning("No 'env' nodes found in the XML");
        }
    }

    private int LoadInt(XmlNode node, string tag, int defaultVal)
    {
        // Load string
        string intString = node.SelectSingleNode(tag).InnerText.Trim();
        // Override default value if possible
        int dafaultValue = defaultVal;
        int.TryParse(intString, out dafaultValue);

        // Return (possibly overridden) default value
        return dafaultValue;
    }

    // Load a choice and return the infos in a dict
    private Dictionary<string, string> LoadChoice(XmlNode choiceNode)
    {
        // Dict to store all the values
        Dictionary<string, string> choiceDict = new Dictionary<string, string>();

        // Load text for choice 1
        string c1text = choiceNode.SelectSingleNode("text").InnerText.Trim();
        //Debug.Log(c1text);
        choiceDict.Add("text", c1text);

        // Load difficulty of succeeding choice 1
        int c1difficulty = LoadInt(choiceNode, "difficulty", 100);
        //Debug.Log(c1difficulty);
        choiceDict.Add("difficulty", "" + c1difficulty); // Cast back to string..

        // Load skill price node
        XmlNode skillpriceNode = choiceNode.SelectSingleNode("skillprice");

        // Load skill type
        string skillType = skillpriceNode.SelectSingleNode("type").InnerText.Trim();
        //Debug.Log("Skill type: " + skillType);
        choiceDict.Add("skillType", skillType);

        // Load amount of skill required
        int skillAmount = LoadInt(skillpriceNode, "amount", 0);
        //Debug.Log("Skill amount: " + skillAmount);
        choiceDict.Add("skillAmount", "" + skillAmount);

        // Load required quest stage
        string requiredQuestStage = choiceNode.SelectSingleNode("requiredQuestStage").InnerText.Trim();
        //Debug.Log(requiredQuestStage);
        choiceDict.Add("requiredQuestStage", requiredQuestStage);



        ///////////// Good outcome /////////////


        // Load good outcome node for choice 1
        XmlNode goodOutcome = choiceNode.SelectSingleNode("goodOutcome");

        // Load dialogue text
        string goodDialogueText = goodOutcome.SelectSingleNode("dialogue/text").InnerText.Trim();
        //Debug.Log(goodDialogueText);
        choiceDict.Add("goodDialogueText", goodDialogueText);

        // Load achievement for good outcome
        string goodGainedAchievement = goodOutcome.SelectSingleNode("gainedAchievement").InnerText.Trim();
        //Debug.Log(goodGainedAchievement);
        choiceDict.Add("goodGainedAchievement", goodGainedAchievement);

        // Load object for good outcome
        string goodSetQuestStage = goodOutcome.SelectSingleNode("setQuestStage").InnerText.Trim();
        //Debug.Log(goodSetQuestStage);
        choiceDict.Add("goodSetQuestStage", goodSetQuestStage);

        // Load object for good outcome
        string goodGainedObject = goodOutcome.SelectSingleNode("gainedObject").InnerText.Trim();
        //Debug.Log(goodGainedObject);
        choiceDict.Add("goodGainedObject", goodGainedObject);

        // Load skill price node for good outcome
        XmlNode goodOutcomeskillpriceNode = goodOutcome.SelectSingleNode("skillprice");

        // Load skill type
        string goodskillType = goodOutcomeskillpriceNode.SelectSingleNode("type").InnerText.Trim();
        //Debug.Log("Skill type: " + goodskillType);
        choiceDict.Add("goodskillType", goodskillType);

        // Load amount of skill required
        int goodskillAmount = LoadInt(goodOutcomeskillpriceNode, "amount", 0);
        //Debug.Log("Skill amount: " + goodskillAmount);
        choiceDict.Add("goodskillAmount", "" + goodskillAmount);



        ///////////// Bad outcome /////////////

        // Load bad outcome node for choice 1
        XmlNode badOutcome = choiceNode.SelectSingleNode("badOutcome");

        // Load dialogue text
        string badDialogueText = badOutcome.SelectSingleNode("dialogue/text").InnerText.Trim();
        //Debug.Log(badDialogueText);
        choiceDict.Add("badDialogueText", badDialogueText);

        // Load achievement for bad outcome
        string badGainedAchievement = badOutcome.SelectSingleNode("gainedAchievement").InnerText.Trim();
        //Debug.Log(badGainedAchievement);
        choiceDict.Add("badGainedAchievement)t", badGainedAchievement);

        // Load object for bad outcome
        string badSetQuestStage = badOutcome.SelectSingleNode("setQuestStage").InnerText.Trim();
        //Debug.Log(badSetQuestStage);
        choiceDict.Add("badSetQuestStage", badSetQuestStage);

        // Load object for bad outcome
        string badLostObject = badOutcome.SelectSingleNode("lostObject").InnerText.Trim();
        //Debug.Log(badLostObject);
        choiceDict.Add("badLostObject", badLostObject);

        // Load skill price node for bad outcome
        XmlNode badOutcomeskillpriceNode = badOutcome.SelectSingleNode("skillprice");

        // Load skill type
        string badskillType = badOutcomeskillpriceNode.SelectSingleNode("type").InnerText.Trim();
        //Debug.Log("Skill type: " + badskillType);
        choiceDict.Add("badskillType", badskillType);

        // Load amount of skill required
        int badskillAmount = LoadInt(badOutcomeskillpriceNode, "amount", 0);
        //Debug.Log("Skill amount: " + badskillAmount);
        choiceDict.Add("badskillAmount", "" + badskillAmount);


      return choiceDict;
    }


}
