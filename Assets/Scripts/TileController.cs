using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public GameObject event1, event2;

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate events
        DeactivateEvent1();
        DeactivateEvent2();
    }
    

    public void ActivateEvent1()
    {
        event1.SetActive(true);
    }

    public void DeactivateEvent1()
    {
        event1.SetActive(false);
    }

    public void ActivateEvent2()
    {
        event2.SetActive(true);
    }

    public void DeactivateEvent2()
    {
        event2.SetActive(false);
    }

}
