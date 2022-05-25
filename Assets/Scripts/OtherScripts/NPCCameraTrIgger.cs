using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCameraTrIgger : MonoBehaviour
{
    [SerializeField] private bool PlayAudio;
    [SerializeField] private string audioName;
    [SerializeField] private bool isAntoherTrigger = false;
    [SerializeField] private Transform anotherTrigger;
    [SerializeField] private float MinValue = 3f;
    private Mode mode;

    private AudioManager audioManager;
    private CameraFollow cameraFollow;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        cameraFollow = FindObjectOfType<CameraFollow>();
    }
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
                cameraFollow.SetTrigger(transform);
            else
                cameraFollow.SetTrigger(anotherTrigger);

            mode = Mode.Minimizing;

            if(PlayAudio)
                audioManager.SetToMain(audioName);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            cameraFollow.ResetTrigger();
            mode = Mode.Maximizing;

            if(PlayAudio)
            {
                audioManager.StopClipWithDelay(audioName);
                audioManager.PlayClip(audioManager.lastMain.name);
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