using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;
    [Range(0f,1f)]
    public float volume;
    [Range(0f,1f)]
    public float pitch;
    public bool loop;
}
