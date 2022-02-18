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
    private string AudioName;
    void Awake()
    {
        if(playMusicOnStart)
            FindObjectOfType<AudioManager>().PlayClip(AudioName);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(playOnTrigger)
        {
            if(coll.tag == "Player")
            {
               FindObjectOfType<AudioManager>().PlayClip(AudioName);
            }
        }
    }
}
