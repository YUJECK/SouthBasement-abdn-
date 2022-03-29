using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps : MonoBehaviour 
{
    public float fps;
    private void Update()
    {
        fps = 1.0f / Time.deltaTime;
        RatConsole.DisplayText(fps.ToString());
    }
}