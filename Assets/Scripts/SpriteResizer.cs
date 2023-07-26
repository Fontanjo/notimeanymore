using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteResizer : MonoBehaviour
{   
    private float targetWidthInUnits = 12f; // The target width in world units
    private float targetHeightInUnits = 6f; // The target height in world units


    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        //ResizeSprite();
        ResizeToTargetSize();
    }


    private void ResizeToTargetSize()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("SpriteRenderer component not found!");
            return;
        }

        // Calculate the scale needed to match the target size
        float scaleX = targetWidthInUnits / spriteRenderer.bounds.size.x;
        float scaleY = targetHeightInUnits / spriteRenderer.bounds.size.y;

        
        Debug.Log("Target " + targetWidthInUnits);
        Debug.Log("Actual " + spriteRenderer.bounds.size.x);
        Debug.Log("Scale "+ scaleX);


        // Apply the final scale to the sprite
        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

}
