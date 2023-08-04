using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class FinalXMLReader : MonoBehaviour
{
    //public TextMesh textMesh; // Assign a TextMesh component in the Inspector to display the loaded text
    //public string xmlFilePath = "Assets/Resources/data.xml"; // Path to the XML file

    public TextAsset textAsset;

    public Dictionary<string, string> dataDict = new Dictionary<string, string>();

    // Start is called before the first frame update
    void Awake()
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

        // Get root
        XmlNode rootNode = xmlDoc.SelectSingleNode("envs");

        // Get root of other nodes
        XmlNode endNode = rootNode.SelectSingleNode("others/end");


        string leftIntro = endNode.SelectSingleNode("leftIntro").InnerText.Trim();
        dataDict.Add("leftIntro", leftIntro);

        string rigthIntro = endNode.SelectSingleNode("rightIntro").InnerText.Trim();
        dataDict.Add("rigthIntro", rigthIntro);


        string leftForest = endNode.SelectSingleNode("leftForest").InnerText.Trim();
        dataDict.Add("leftForest", leftForest);

        string leftMarsh = endNode.SelectSingleNode("leftMarsh").InnerText.Trim();
        dataDict.Add("leftMarsh", leftMarsh);

        string leftDesert = endNode.SelectSingleNode("leftDesert").InnerText.Trim();
        dataDict.Add("leftDesert", leftDesert);

        string leftMount = endNode.SelectSingleNode("leftMount").InnerText.Trim();
        dataDict.Add("leftMount", leftMount);


        string getHigh = endNode.SelectSingleNode("getHigh").InnerText.Trim();
        dataDict.Add("getHigh", getHigh);

        string partyCrasher = endNode.SelectSingleNode("partyCrasher").InnerText.Trim();
        dataDict.Add("partyCrasher", partyCrasher);

        string sunBathing = endNode.SelectSingleNode("sunBathing").InnerText.Trim();
        dataDict.Add("sunBathing", sunBathing);

        string snowBoarding = endNode.SelectSingleNode("snowBoarding").InnerText.Trim();
        dataDict.Add("snowBoarding", snowBoarding);

        

        string forestQuest = endNode.SelectSingleNode("forestQuest").InnerText.Trim();
        dataDict.Add("ForestQuest", forestQuest);

        string marshQuest = endNode.SelectSingleNode("marshQuest").InnerText.Trim();
        dataDict.Add("MarshQuest", marshQuest);

        string desertQuest = endNode.SelectSingleNode("desertQuest").InnerText.Trim();
        dataDict.Add("DesertQuest", desertQuest);

        string mountQuest = endNode.SelectSingleNode("mountQuest").InnerText.Trim();
        dataDict.Add("MountQuest", mountQuest);
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


}
