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
    new private Camera camera;
    private CameraFollow cameraFollow;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        camera = Camera.main;
        cameraFollow = FindObjectOfType<CameraFollow>();
    }
    void OnLevelWasLoaded()
    {
        if(camera.orthographicSize <= MinValue)
            mode = Mode.Maximizing;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(!isAntoherTrigger)
                cameraFollow.SetTarget(transform);
            else
                cameraFollow.SetTarget(anotherTrigger);

            mode = Mode.Minimizing;

            if(PlayAudio)
                audioManager.SetToMain(audioName);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            cameraFollow.ResetTarget();
            mode = Mode.Maximizing;

            if(PlayAudio)
                audioManager.SetToMain(audioManager.lastMain.name);
        }
    }

    void Update()
    {
        if(mode == Mode.Maximizing)
        {
            camera.orthographicSize += 0.2f;

            if(camera.orthographicSize > 6f)
                mode = Mode.None;
        }
        if(mode == Mode.Minimizing)
        {
            camera.orthographicSize -= 0.2f;  
            if(camera.orthographicSize < MinValue)
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