using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LevelGenerator : MonoBehaviour
{

    //[Header("Images")]
    [Tooltip("The prefabs from which to generate the scenes")]
    public List<GameObject> imagePrefabs;

    [Space(10)]

    [Tooltip("The object under which all instantiated images will be")]
    public Transform parentGameObject;


    [Tooltip("The number of layers visible at each time")]
    [Range(2,10)]
    public int numberOfLayers = 3;

    // Position of the first image, probably   new Vector3(0, 0, 0)
    private Vector3 initialImagePosition = new Vector3(0, -3, 0);
    public static Vector3 leftMovement = new Vector3(-11, 4, 10);
    public static Vector3 rightMovement = new Vector3(11, 4, 10);


    // !!!!!!!! static field !!!!!!!!!!
    // There will always exist only one root node
    // If multiple classes of LevelGenerator are instantiated in the same scene, something bad is happening
    // Don't do that
    // Really
    private static ImageNode rootNode;

   

    /// Start is called before the first frame update
    /// Generate first 3 layers, and keep track of the root node
    void Start()
    {
        rootNode = GenerateRandomLevel();
    }

    /// Generate a random sequence of images
    private ImageNode GenerateRandomLevel()
    {
        // Generate first image at the center
        GameObject mainImageObject = InstantiateRandomPrefab(imagePrefabs, initialImagePosition);
        ImageNode rootNode = new ImageNode(mainImageObject);

        // Get the nodes of the current layer (at the first iteration only the root node)
        List<ImageNode> currentLayer = new List<ImageNode>();
        currentLayer.Add(rootNode);

        
        // Compute the step of the following image when adding to the right
        Vector3 rmove = rightMovement - leftMovement;
        // Generate following layers
        for (int i = 2; i <= numberOfLayers; i++)
        {
            // Compute the position of the leftmost image in the new layer
            Vector3 pos = currentLayer[0].position + leftMovement;

            // Store the new images
            List<ImageNode> newLayer = new List<ImageNode>(); 

            // Generate the nodes in the next layer
            for (int j = 0; j < i; j++)
            {
                // Generate and save prefab
                GameObject pref = InstantiateRandomPrefab(imagePrefabs, pos);
                ImageNode node = new ImageNode(pref);

                newLayer.Add(node);
                
                // Update position for next element
                pos += rmove;
            }

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
        

        // Return the root node
        return rootNode;
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
    }


    // Delete node fro list, and object from scene
    private static void DeleteNode(ImageNode node)
    {
        // Delete object from scene
        Destroy(node.imageObject);
    }
}
