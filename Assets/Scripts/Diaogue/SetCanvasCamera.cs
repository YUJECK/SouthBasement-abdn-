using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvasCamera : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Canvas>().worldCamera = FindObjectOfType<CameraFollow>().Camera.GetComponent<Camera>();
    }
}
