using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenResolution : MonoBehaviour
{   
    [SerializeField] private int Width= 1070;
    [SerializeField] private int Height = 720;
    public Camera FrontFacingCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        print(FrontFacingCamera.pixelHeight);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
