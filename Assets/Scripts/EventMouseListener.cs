using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMouseListener : MonoBehaviour
{
    // Sprite Renderer component of the object
    private SpriteRenderer sr;
    private Color origCol, onMouseOverCol;


    private CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        /////////////////////////////////////////
        /////////////////////////////////////////
        // Make sure sprite is the first child //
        /////////////////////////////////////////
        /////////////////////////////////////////
        sr = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        // Get the script controlling the main camera
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();

        // Save the original color
        origCol = sr.color;

        // Generate color to apply when mouse over
        onMouseOverCol = new Color(origCol.r * 0.8f, origCol.g * 0.8f, origCol.b * 0.8f, origCol.a);
    }


    void OnMouseDown(){
        //Debug.Log(sr.color);
        if (Selectable())
        {
            Debug.Log("Event clicked");

            // Block selecting events
            LevelVariables.Instance().BlockSelectEvent();

            // Play event choosen
            PlayEvent();

            // Continue in game
            cameraController.ZoomOut();
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

    // With the current logic, always selectable when active
    public bool Selectable()
    {
        return LevelVariables.Instance().CanSelectEvent();
    }

    private void PlayEvent()
    {
        Debug.Log("Playing event on " + gameObject.name);
    }
}
