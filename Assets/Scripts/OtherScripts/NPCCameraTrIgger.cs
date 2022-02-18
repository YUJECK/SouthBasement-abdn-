using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCameraTrIgger : MonoBehaviour
{
    [SerializeField]
    private bool PlayAudio;

    [SerializeField]
    private bool stopWithDelay;

    [SerializeField]
    private string audioName;

    [SerializeField]
    private bool isAntoherTrigger = false;

    [SerializeField]
    private Transform anotherTrigger;

    [SerializeField]
    private float MinValue = 3f;
    private Mode mode;

    void OnLevelWasLoaded()
    {
        if(FindObjectOfType<CameraFollow>().Camera.GetComponent<Camera>().orthographicSize <= MinValue)
            mode = Mode.Maximizing;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(!isAntoherTrigger)
                FindObjectOfType<CameraFollow>().SetTrigger(transform);
            else
                FindObjectOfType<CameraFollow>().SetTrigger(anotherTrigger);

            mode = Mode.Minimizing;

            if(PlayAudio)
                FindObjectOfType<AudioManager>().PlayClip(audioName);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {                
            FindObjectOfType<CameraFollow>().ResetTrigger();
            mode = Mode.Maximizing;

            if(PlayAudio)
            {
                if(stopWithDelay)
                    FindObjectOfType<AudioManager>().StopClipWithDelay(audioName);
                
                else
                    FindObjectOfType<AudioManager>().StopClip(audioName);
            }
        }
    }

    void Update()
    {
        if(mode == Mode.Maximizing)
        {
            FindObjectOfType<CameraFollow>().Camera.GetComponent<Camera>().orthographicSize += 0.2f;

            if(FindObjectOfType<CameraFollow>().Camera.GetComponent<Camera>().orthographicSize > 6f)
                mode = Mode.None;
        }
        if(mode == Mode.Minimizing)
        {
            FindObjectOfType<CameraFollow>().Camera.GetComponent<Camera>().orthographicSize -= 0.2f;  
            if(FindObjectOfType<CameraFollow>().Camera.GetComponent<Camera>().orthographicSize < MinValue)
                mode = Mode.None;
        }
    }

    private enum Mode
    {
        None,
        Minimizing,
        Maximizing
    };
}