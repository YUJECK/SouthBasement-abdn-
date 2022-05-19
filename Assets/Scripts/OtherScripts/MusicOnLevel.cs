using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnLevel : MonoBehaviour
{
    //Settings
    [SerializeField] private bool playMusicOnStart;

    [SerializeField] private bool playOnTrigger;

    [SerializeField] private string[] AudioNames;
    
    //Managers
    private AudioManager audioManager;
    void Start()
    {
        if(playMusicOnStart)
            audioManager.SetToMain(AudioNames[Random.Range(0, AudioNames.Length)]);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(playOnTrigger)
        {
            if(coll.tag == "Player")
               audioManager.PlayClip(AudioNames[Random.Range(0, AudioNames.Length)]);
        }
    }
}
