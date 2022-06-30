using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{   
    [Header("Scripts Ref:")]
    public MainCamera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D other) {
        if(mainCamera.cameraFreeWalk.fieldOfView == mainCamera.zoomIn)
            mainCamera.cameraFreeWalk.fieldOfView = mainCamera.zoomOut;
        else
            mainCamera.cameraFreeWalk.fieldOfView = mainCamera.zoomIn;
    }
    
}
