using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class CameraController : MonoBehaviour
{

    Transform cameraTransform; 
    Vector3 target;
    public float duration = 20f;


    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.gameObject.transform;
        target = new Vector3(cameraTransform.transform.position.x + 6, cameraTransform.transform.position.y + 3, cameraTransform.transform.position.z + 20);

    }

    float time = 0f;

    // Update is called once per frame
    void Update()
    {
        
        float t = time / duration;
        t = t * t * (3f - 2f * t);

        //cameraTransform.Translate(6/step, 3/step, 20/step);
        cameraTransform.transform.position = Vector3.Lerp(cameraTransform.transform.position, target, t);

        time += Time.deltaTime;
    }

    

}

