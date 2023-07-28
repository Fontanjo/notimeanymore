using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RestartButtonListener : MonoBehaviour
{
    void Restart(){
        Debug.Log("Restart");
        GlobalVariables.Set("currentLevelIndex", 2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
    }


    private UnityAction act;

    void Start()
    {
       Button bt = GetComponent<Button>();

       act += Restart;

       bt.onClick.AddListener(act);
    }

}
