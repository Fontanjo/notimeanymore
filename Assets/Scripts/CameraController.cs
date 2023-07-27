using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraController : MonoBehaviour
{

    Transform cameraTransform; 
    private Vector3 rightMovement, leftMovement;
    Vector3 targetRight, targetLeft;
    public float duration = 20f;

    public bool canMove;

    float time = 0f;

    private GameObject levelGeneratorObj;
    private LevelGenerator levelGenerator;
    

    // Start is called before the first frame update
    void Start()
    {
        // Get the position of the camera
        cameraTransform = Camera.main.gameObject.transform;

        // Assign movement to get to the new sprite
        //rightMovement = new Vector3(6, 3, 5);
        //leftMovement = new Vector3(-6, 3, 5);
        rightMovement = LevelGenerator.rightMovement;
        leftMovement = LevelGenerator.leftMovement;

        // Create target coordinates
        UpdateTargets();

        // Get reference to level generator
        // Ideally this should not be necessary, refactor level generator script into singleton if possible
        levelGeneratorObj = GameObject.Find("LevelGenerator");
        levelGenerator = levelGeneratorObj.GetComponent<LevelGenerator>();

        // Signal that the camera is ready to move
        canMove = true;
    }

    // Move to the sprite on the right
    public void MoveRight()
    {
        // Move camera
        StartCoroutine(_moveTo(targetRight, "right"));
    }

    // Move to the sprite on the left
    public void MoveLeft()
    {
        // Move camera
        StartCoroutine(_moveTo(targetLeft, "left"));  
    }


    // Actual movement coroutine
    private IEnumerator _moveTo(Vector3 targetPos, string dir)
    {

        // Get tile controller of starting tile (root not yet updated)
        TileController oldTc = LevelGenerator.GetRootNode().imageObject.GetComponent<TileController>();

        // Show animation
        Animator oldAnim = oldTc.GetWizardAnimator();
        if (oldAnim != null)
        {
            oldAnim.SetTrigger("IsLeaving");
            yield return new WaitForSeconds(3.5f);
        }

        
        // Block additional movement
        canMove = false;
        while (Vector3.Distance(cameraTransform.transform.position, targetPos) > 0.01f)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            //cameraTransform.Translate(6/step, 3/step, 20/step);
            cameraTransform.transform.position = Vector3.Lerp(cameraTransform.transform.position, targetPos, t);

            time += Time.deltaTime;
            yield return null;
        }

        

        // Update targets
        UpdateTargets(); 
        // Update root node and delete deprecated objects
        if (dir == "left")
            LevelGenerator.MoveRootNodeLeft();
        if (dir == "right")
            LevelGenerator.MoveRootNodeRight();

        

        // Get tile controller
        TileController tc = LevelGenerator.GetRootNode().imageObject.GetComponent<TileController>();

        // Show arrival animation
        tc.ActivateWizard();
        tc.GetWizardAnimator().SetTrigger("IsMoving");
        yield return new WaitForSeconds(1);

        // Zoom on the selected image
        ZoomIn(tc);

        // Play situation
        PlayTile();

        /* // Go back to map view
        ZoomOut(); */

        
    }


    /// Zoom on the selected image
    private void ZoomIn(TileController tc)
    {
        Debug.Log("Zoom in");

        if (tc == null)
        {
            Debug.Log("Tile Controller not found!");
        }
        else
        {
            tc.ActivateEvent1();
            //tc.ActivateEvent2(); // For now always only 1, and player directly
        }
    }

    /// Zoom out of the selected image
    public void ZoomOut()
    {
        Debug.Log("Zoom back to map");

        // Allow to move again
        canMove = true;

        LevelVariables.Instance().AllowMovement();
    }

    /// Play on the current tile (root node)
    /// From the tile itself, get new objects and place them
    /// Add click listener and game logic
    public void PlayTile()
    {
        Debug.Log("Play event1 on tile " + LevelGenerator.GetRootNode().imageObject.name);

        string[] lines = {"Choice dialogue line 1", "Choice dialogue line 2"};
        levelGenerator.NewChoiceDialogueBox(lines);

    }

    private void UpdateTargets()
    {
        // Make sure the camera reference is updated
        cameraTransform = Camera.main.gameObject.transform;

        // Add right/left deplacement
        targetRight = cameraTransform.transform.position + rightMovement;
        targetLeft  = cameraTransform.transform.position + leftMovement;

        // Reset time
        time = 0f;
    }

    

}

