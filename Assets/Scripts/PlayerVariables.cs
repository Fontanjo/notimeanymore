using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables
{
    // Singleton, allow only 1 instance of the class
    public static PlayerVariables Instance()
    {
        if (_Instance == null)
            _Instance = new PlayerVariables();
        return _Instance;
    }
   
    private static PlayerVariables _Instance;

    private int bodyDices;
    private int soulDices;
    private int brainDices;


    public int GetBodyDices()
    {
        return bodyDices;
    }

    public void SetBodyDices(int dices)
    {
        bodyDices = dices;
    }


    public int GetSoulDices()
    {
        return soulDices;
    }

    public void SetSouldDices(int dices)
    {
        soulDices = dices;
    }


    public int GetBrainDices()
    {
        return brainDices;
    }

    public void SetBrainDices(int dices)
    {
        brainDices = dices;
    }
}
