using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVariables
{
    // Singleton, allow only 1 instance of the class
    public static LevelVariables Instance()
    {
        if (_Instance == null)
            _Instance = new LevelVariables();
        return _Instance;
    }
   
    private static LevelVariables _Instance;
    private bool canMove = false;
    private bool canSelectEvent = false;



    public bool CanMove()
    {
        return canMove;
    }

    public void AllowMovement()
    {
        Debug.Log("Movement unlocked");
        canMove = true;
    }

    public void BlockMovement()
    {
        Debug.Log("Movement blocked");
        canMove = false;
    }


    public bool CanSelectEvent()
    {
        return canSelectEvent;
    }

    public void AllowSelectEvent()
    {
        Debug.Log("Event selection unlocked");
        canSelectEvent = true;
    }

    public void BlockSelectEvent()
    {
        Debug.Log("Event selection blocked");
        canSelectEvent = false;
    }
}
