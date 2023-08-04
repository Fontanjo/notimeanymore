using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndButtonListener : MonoBehaviour
{
    void End(){
        Debug.Log("End");
        Application.Quit();
    }


    private UnityAction act;

    void Start()
    {
       Button bt = GetComponent<Button>();

       act += End;

       bt.onClick.AddListener(act);
    }

}
