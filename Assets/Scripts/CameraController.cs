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
        // Allow to move again
        canMove = true;
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

