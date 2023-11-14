using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerController : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera;

    private void OnEnable() { FindCamera(); }
    private void FindCamera() { sceneCamera = FindObjectOfType<Camera>(); }

    private void FixedUpdate()
    {
        if (sceneCamera == null)
            FindCamera();
        
        transform.position = sceneCamera.transform.position;
    }
}
