using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationSettings : MonoBehaviour
{
    [SerializeField]
    private bool playMusicOnStart;

    [SerializeField]
    private bool playOnTrigger;

    [SerializeField]
    private string[] AudioNames;
    void Start()
    {
        if(playMusicOnStart)
            FindObjectOfType<AudioManager>().PlayClip(AudioNames[Random.Range(0, AudioNames.Length)]);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(playOnTrigger)
        {
            if(coll.tag == "Player")
            {
               FindObjectOfType<AudioManager>().PlayClip(AudioNames[Random.Range(0, AudioNames.Length)]);
            }
        }
    }
}
