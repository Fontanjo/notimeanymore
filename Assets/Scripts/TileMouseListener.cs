using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMouseListener : MonoBehaviour
{
    // Sprite Renderer component of the object
    private SpriteRenderer sr;
    private Color origCol, onMouseOverCol;

    private CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        // Get the script controlling the main camera
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();

        // Get renderer you want to probe
        sr = GetComponent<SpriteRenderer>();

        // Save the original color
        origCol = sr.color;

        // Generate color to apply when mouse over
        onMouseOverCol = new Color(origCol.r * 0.8f, origCol.g * 0.8f, origCol.b * 0.8f, origCol.a);
    }


    void OnMouseDown(){
        //Debug.Log(sr.color);
        if (Selectable())
        {
            string direction = ImageNode.GetPath(gameObject);

            // Convert to uppercase to make the switch case-insensitive
            switch (direction.ToUpper())
            {
                case "LEFT":
                    cameraController.MoveLeft();
                    break;
                case "RIGHT":
                    cameraController.MoveRight();
                    break;
                case "NOT FOUND":
                    // Code to execute when direction is "HERE"
                    Debug.Log("No path was found between target and current node");
                    break;
                case "SAME":
                    // Code to execute when direction is "HERE"
                    Debug.Log("You are already at the target");
                    break;
                default:
                    // Code to execute when the direction is none of the specified cases
                    Debug.Log("Error in finding the direction ");
                    break;
            }
        }

    }

    void OnMouseEnter(){
        // Only make selectable when
        if (Selectable())
            sr.color = onMouseOverCol;
    }

    void OnMouseExit(){
        sr.color = origCol;
    }

    // Return true if this tile can be selected and the camera can move to it
    public bool Selectable()
    {
        bool b1 = false;
        if (cameraController != null)
            b1 = cameraController.canMove;
        bool b2 = ImageNode.GetTileDepth(gameObject) == 1;
        bool b3 = LevelVariables.Instance().CanMove();
        return (b1 && b2 && b3);
        //return (cameraController.canMove && ImageNode.GetTileDepth(gameObject) == 1 && LevelVariables.Instance().CanMove());
    }
}
