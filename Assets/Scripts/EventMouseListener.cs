using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMouseListener : MonoBehaviour
{
    // Sprite Renderer component of the object
    private SpriteRenderer sr;
    private Color origCol, onMouseOverCol;


    private CameraController cameraController;

    private GameObject levelGeneratorObj;
    private LevelGenerator levelGenerator;

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

        // Get reference to level generator
        // Ideally this should not be necessary, refactor level generator script into singleton if possible
        levelGeneratorObj = GameObject.Find("LevelGenerator");
        levelGenerator = levelGeneratorObj.GetComponent<LevelGenerator>();
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
            //cameraController.ZoomOut();
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

    public void PlayEvent()
    {
        Debug.Log("Playing event on " + gameObject.name);
        string[] lines = {"Choice dialogue line 1", "Choice dialogue line 2"};
        string[] choices = {"EvListener c1", "EvListener c2", "EvListener c3"};
        levelGenerator.NewChoiceDialogueBox(lines, choices);
    }
}
