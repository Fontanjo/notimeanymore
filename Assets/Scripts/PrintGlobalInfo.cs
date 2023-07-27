using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintGlobalInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int levelIndex = GlobalVariables.Get<int>("currentLevelIndex");
        Debug.Log(levelIndex);
    }
}
