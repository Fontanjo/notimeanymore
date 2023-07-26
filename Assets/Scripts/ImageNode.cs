using UnityEngine;

// Class representing each image node in the binary tree
public class ImageNode
{
    public GameObject imageObject;
    public ImageNode leftChild;
    public ImageNode rightChild;
    public Vector3 position;

    //private static LevelGenerator lg;

    void Start()
    {
        // Get the level generator
      //  lg = GameObject.FindWithTag("LevelGenerator").GetComponent<LevelGenerator>();
    }

    public ImageNode(GameObject imageObject)
    {
        this.imageObject = imageObject;
        position = this.imageObject.transform.position;
        leftChild = null;
        rightChild = null;
    }


    // Get the direction (left/right) to go from a given node "rootNode" to the target object
    public static string GetPath(ImageNode rootNode, GameObject targetobject)
    {
        // If the root and the target already correspond
        if (rootNode.imageObject == targetobject)
            return "SAME";

        // If found in the left path
        if (RecursiveTraverse(rootNode.leftChild, targetobject))
            return "LEFT";

        // If found in the right path
        if (RecursiveTraverse(rootNode.rightChild, targetobject))
            return "RIGHT";

        // If not found neither left not right
        return "NOT FOUND";
    }

    // Same as before, using the root indicated by the level generator
    public static string GetPath(GameObject targetobject)
    {
        return GetPath(LevelGenerator.GetRootNode(), targetobject);
    }

    /// Get the depth in the scene of a tile
    /// 0 if it's in the first plane
    /// 1 if it's in the second plane (thus selectable)
    /// 2 if it's behind second plane
    /// -1 if not on the path from root node
    public static int GetTileDepth(GameObject targetobject)
    {
        // Get root node
        ImageNode rootNode = LevelGenerator.GetRootNode();

        // If the root and the target already correspond
        if (rootNode.imageObject == targetobject)
            return 0;

        // If found as left child
        if (rootNode.leftChild.imageObject == targetobject)
            return 1;

            // If found as left child
        if (rootNode.rightChild.imageObject == targetobject)
            return 1;

        if (RecursiveTraverse(rootNode.rightChild, targetobject) || RecursiveTraverse(rootNode.leftChild, targetobject))
            return 2;

        // If not found neither left not right
        return -1;
    }

    // Recursivly search through the tree
    private static bool RecursiveTraverse(ImageNode node, GameObject targetobject)
    {
        // If we reach the end of the tree and the target was not found, return false
        if (node == null)
            return false;

        // Compare gameObjects to evaluate if they are the same
        if (node.imageObject == targetobject)
            return true;

        // If it is found in either the left of the right path, return true
        // Recursive call
        return RecursiveTraverse(node.leftChild, targetobject) || RecursiveTraverse(node.rightChild, targetobject);
    }
}