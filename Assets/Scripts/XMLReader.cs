using UnityEngine;
using System.Xml;

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

        // Find the text node
        XmlNode textNode = xmlDoc.SelectSingleNode("data/text");
        if (textNode != null)
        {
            string externalText = textNode.InnerText;
            Debug.Log("External text: " + externalText);

            // Display the loaded text in the TextMesh component
            /* if (textMesh != null)
            {
                textMesh.text = externalText;
            } */
        }
        else
        {
            Debug.LogWarning("Text node not found in the XML.");
        }
    }
}