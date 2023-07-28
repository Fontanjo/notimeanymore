using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelGenerator : MonoBehaviour
{

    //[Header("Images")]
    [Tooltip("The prefabs from which to generate the scenes")]
    public List<GameObject> imagePrefabs;
    public List<GameObject> initialPrefabs;

    [Space(10)]

    [Tooltip("The object under which all instantiated images will be")]
    public Transform parentGameObject;

    [Space(10)]
    [Tooltip("The object under which all instantiated UI elements will be")]
    public Transform canvasParentObject;


    [Tooltip("The number of layers visible at each time")]
    [Range(2,10)]
    public int numberOfLayers = 3;

    [Space(10)]
    public GameObject dialogueboxObject;
    public GameObject choiceDialogueboxObject;

    private Dialogue dialogueScript;
    private ChoiceDialogue choiceDialogueScript;


    [Space(10)]
    // Position of the first image, probably   new Vector3(0, 0, 0)
    public static Vector3 initialImagePosition = new Vector3(0, -4.2f, 0);
    public static Vector3 leftMovement = new Vector3(-11, 3, 10);
    public static Vector3 rightMovement = new Vector3(11, 3, 10);


    // !!!!!!!! static field !!!!!!!!!!
    // There will always exist only one root node
    // If multiple classes of LevelGenerator are instantiated in the same scene, something bad is happening
    // Don't do that
    // Really
    private static ImageNode rootNode;

    // Reference to last layer
    private static List<ImageNode> lastLayer;

    private static LevelGenerator thisScript;

    private static Dictionary<string, Dictionary<string, Dictionary<string, string>>> dialoguesDataDict;

    /// Start is called before the first frame update
    /// Generate first 3 layers, and keep track of the root node
    void Start()
    {
        rootNode = GenerateRandomLevel();

        // Get the script of the dialogue
        dialogueScript = dialogueboxObject.GetComponent<Dialogue>();
        choiceDialogueScript = choiceDialogueboxObject.GetComponent<ChoiceDialogue>();


        // Well there should be a better way but as long as it works..
        // The goal is to have a reference to this instance for static function
        // Necessary e.g. to instantiate new tiles
        thisScript = GetComponent<LevelGenerator>();

    }

    /// Generate a random sequence of images
    private ImageNode GenerateRandomLevel()
    {
        // Generate first image at the center
        GameObject mainImageObject = InstantiateRandomPrefab(initialPrefabs, initialImagePosition);
        ImageNode rootNode = new ImageNode(mainImageObject);

        // Get the nodes of the current layer (at the first iteration only the root node)
        List<ImageNode> currentLayer = new List<ImageNode>();
        currentLayer.Add(rootNode);

        // Compute the step of the following image when adding to the right
        Vector3 rmove = rightMovement - leftMovement;
        // Generate following layers
        for (int i = 2; i <= numberOfLayers; i++)
        {
            // Generate new layer
            Vector3 initPos = currentLayer[0].position + leftMovement;
            List<ImageNode> newLayer = GenerateNewLayer(i, initPos, rmove);

            // Check everything is fine
            Assert.IsTrue(currentLayer.Count + 1 == newLayer.Count);

            // Connect layers
            for (int k = 0; k < currentLayer.Count; k++)
            {
                currentLayer[k].leftChild = newLayer[k];
                currentLayer[k].rightChild = newLayer[k+1];
            }

            // Move to next layer
            currentLayer = newLayer;
        }

        // Save reference to last layer, to simplify addition of new layers later
        lastLayer = currentLayer;

        // Return the root node
        return rootNode;
    }

    /// Generate a new layer of tiles
    /// The layer will be composed of "nbTiles" new tiles
    /// The first element will be at "initialPos" position
    /// Every followig tile will be deplaced by "rightMovement"
    private List<ImageNode> GenerateNewLayer(int nbTiles, Vector3 initialPos, Vector3 rightMovement)
    {
        // Compute the position of the leftmost image in the new layer
        Vector3 pos = initialPos;

        // Store the new images
        List<ImageNode> newLayer = new List<ImageNode>();

        // Generate the nodes in the next layer
        for (int j = 0; j < nbTiles; j++)
        {
            // Generate and save prefab
            GameObject pref = InstantiateRandomPrefab(imagePrefabs, pos);
            ImageNode node = new ImageNode(pref);

            newLayer.Add(node);

            // Update position for next element
            pos += rightMovement;
        }

        return newLayer;
    }


    /// Instantiate a random prefab from a given list, and at a given position
    private GameObject InstantiateRandomPrefab(List<GameObject> prefabList, Vector3 position)
    {
        // Ensure there are choices
        if (prefabList.Count == 0)
        {
            Debug.LogWarning("Prefab list is empty. Please add prefabs to the list.");
            return null;
        }

        // Randomly choose one
        int randomIndex = Random.Range(0, prefabList.Count);
        GameObject randomPrefab = prefabList[randomIndex];
        // Instantiate it
        GameObject instantiatedPrefab = Instantiate(randomPrefab, position, Quaternion.identity);
        // Set the parent object, to keep all the generated object in the same place
        instantiatedPrefab.transform.parent = parentGameObject;

        // Return the new object
        return instantiatedPrefab;
    }

    // Return the ONLY existing root not (it is static!)
    public static ImageNode GetRootNode()
    {
        return rootNode;
    }

    // Update the root node, and delete unreachable tiles
    public static void MoveRootNodeRight()
    {
        // Delete all the noded on the rightmost path
        ImageNode current = rootNode.leftChild;
        while (current != null)
        {
            // Save ref to old root
            ImageNode toDelete = current;

            // Update root node
            current = current.leftChild;

            //Debug.Log("Delete " + toDelete.imageObject.name);

            DeleteNode(toDelete);
        }

        // Save ref to old root
        ImageNode tempRootRef = rootNode;

        // Update root node
        rootNode = rootNode.rightChild;

        DeleteNode(tempRootRef);

        // Update last array
        lastLayer.RemoveAt(0);

        // Generate new layer
        thisScript.AttachNewLayer();
    }

    // Update the root node, and delete unreachable tiles
    public static void MoveRootNodeLeft()
    {
        // Delete all the noded on the rightmost path
        ImageNode current = rootNode.rightChild;
        while (current != null)
        {
            // Save ref to old root
            ImageNode toDelete = current;

            // Update root node
            current = current.rightChild;

            //Debug.Log("Delete " + toDelete.imageObject.name);

            DeleteNode(toDelete);
        }

        // Save ref to old root
        ImageNode tempRootRef = rootNode;

        // Update root node
        rootNode = rootNode.leftChild;

        DeleteNode(tempRootRef);

        // Update last array
        lastLayer.RemoveAt(lastLayer.Count - 1);

        // Generate new layer
        thisScript.AttachNewLayer();
    }

    // Generate a new layer and attach it to the tree
    private void AttachNewLayer()
    {
        int nbTiles = lastLayer.Count + 1; // Even if one element was deleted, the array size is the same
/*         Debug.Log(nbTiles);
        Debug.Log(lastLayer); */

        // Initial position
        Vector3 startPos = lastLayer[0].position + leftMovement;

        // Compute the step of the following image when adding to the right
        Vector3 rmove = rightMovement - leftMovement;

        // Copy reference to last layer
        List<ImageNode> currentLayer = lastLayer;

        // Generate new layer
        List<ImageNode> newLayer = GenerateNewLayer(nbTiles, startPos, rmove);

        // Check everything is fine
        Assert.IsTrue(currentLayer.Count + 1 == newLayer.Count);

        // Connect layers
        for (int k = 0; k < currentLayer.Count; k++)
        {
            currentLayer[k].leftChild = newLayer[k];
            currentLayer[k].rightChild = newLayer[k+1];
        }

        // Update reference to last layer
        lastLayer = newLayer;

    }


    // Delete node fro list, and object from scene
    private static void DeleteNode(ImageNode node)
    {
        // Delete object from scene
        Destroy(node.imageObject);
    }

    // Start a new dialogue
    // Is in this script to have a single point to handle all the level
    // And a single object to which add all the objects in the editor
    public void NewDialogueBox(string[] sentences, string[] allowAfter)
    {
        // Start new dialogue
        dialogueScript.NewDialogue(sentences, allowAfter);
    }


    // Start a new dialogue with choices
    // Is in this script to have a single point to handle all the level
    // And a single object to which add all the objects in the editor
    public void NewChoiceDialogueBox(Dictionary<string, Dictionary<string, string>> dialogueDict)
    {
        // Start new dialogue
        choiceDialogueScript.NewDialogue(dialogueDict);
    }




    // Set dialogue data
    public static void SetDialoguesDataDict(Dictionary<string, Dictionary<string, Dictionary<string, string>>> dict)
    {
        dialoguesDataDict = dict;
    }

    // Get dialogue data dict
    public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> GetDialoguesDataDict()
    {
        return dialoguesDataDict;
    }
}
