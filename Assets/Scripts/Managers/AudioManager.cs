using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios;

    public static AudioManager instance;

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Audio audio in audios)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
            audio.source.pitch = audio.pitch;
            audio.source.loop = audio.loop;
        }
    }

    private void OnLevelWasLoaded()
    {
        foreach(Audio audio in audios)
        {
            if(audio.source.isPlaying)
                StopClipWithDelay(audio.name);
        }
    }

    public void PlayClip(string name)
    {  
        Audio au = null;

        for(int i = 0; i < audios.Length; i++)
        {
            if(audios[i].name == name)
                au = audios[i];
        }
        
        if(au == null)
        {
            Debug.LogWarning("Audio " + name + " not fount");
            return;
        }

        if(!au.source.isPlaying)
            au.source.Play();
    }
    public void StopClip(string name)
    {  
        Audio au = null;
        for(int i = 0; i < audios.Length; i++)
        {
            if(audios[i].name == name)
                au = audios[i];
        }

        if(au.source.isPlaying)
            au.source.Stop();
        else
            Debug.LogWarning("Audio " + name + " isn't plaing");
    }
    public void StopClipWithDelay(string name)
    {  
        Audio au = null;

        for(int i = 0; i < audios.Length; i++)
        {
            if(audios[i].name == name)
                au = audios[i];
        }

        if(au.source.isPlaying)
            StartCoroutine("stopWithDelay", au);
        else
            Debug.LogWarning("Audio " + name + " isn't plaing");
    }

    private IEnumerator stopWithDelay(Audio au)
    {
        for(int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.1f);
            au.source.volume -= 0.03f;
        }
        
        au.source.Stop();
        au.source.volume = au.volume;
    }
}
