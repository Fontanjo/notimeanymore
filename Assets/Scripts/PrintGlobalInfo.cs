using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrintGlobalInfo : MonoBehaviour
{

    public TextMeshProUGUI textComponent;


    // Start is called before the first frame update
    void Start()
    {
        int levelIndex = GlobalVariables.Get<int>("currentLevelIndex");
        Debug.Log(levelIndex);

        textComponent.text = "Level: " + levelIndex;
    }
}
