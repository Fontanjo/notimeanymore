using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public GameObject wizard, event1, event2;

    public string tileId = "defaultTile";

    // Start is called before the first frame update
    void Start()
    {
        // Deactivate events
        DeactivateEvent1();
        DeactivateEvent2();
        DeactivateWizard();
    }
    

    public void ActivateWizard()
    {
        if (wizard != null)
        {
            wizard.SetActive(true);
        }
    }

    public void DeactivateWizard()
    {
        if (wizard != null)
        {
            wizard.SetActive(false);
        }
    }

    public Animator GetWizardAnimator()
    {
        if (wizard != null)
        {
            return wizard.gameObject.transform.GetChild(0).GetComponent<Animator>();
        }
        else
        {
            return null;
        }
        
    }


    public void ActivateEvent1()
    {
        if (event1 != null)
        {
            event1.SetActive(true);
        }
    }

    public void DeactivateEvent1()
    {
        if (event1 != null)
        {
            event1.SetActive(false);
        }
    }

    public void ActivateEvent2()
    {
        if (event2 != null)
        {
            event2.SetActive(true);
        }
    }

    public void DeactivateEvent2()
    {
        if (event2 != null)
        {
            event2.SetActive(false);
        }
    }


}
