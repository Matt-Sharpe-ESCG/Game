using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    // Camera Refrences
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;

    // Logic Bool
    public bool cameraSwitched = false;

    // Starting POV
    public void Start()
    {
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);
    }

    public void Update()
    {
        // Switch Camera POV to 1st Person
        if (Input.GetKeyDown(KeyCode.F))
        {
            firstPersonCamera.SetActive(true);
            thirdPersonCamera.SetActive(false);
        }
        // Switch Camera POV to 3rd Person
        else if (Input.GetKeyDown(KeyCode.T))
        {
            firstPersonCamera.SetActive(false);
            thirdPersonCamera.SetActive(true);
        }
    }
}
