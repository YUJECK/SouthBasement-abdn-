using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios; // Все аудио в игре

    public static AudioManager instance; // Синглтон

    void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach(Audio audio in audios) // "Активация" всех аудио
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
        //Останавливаем все аудио при переходе на след уровень
        foreach(Audio audio in audios)
        {
            if(audio.source.isPlaying)
                StopClipWithDelay(audio.name);
        }
    }

    public void PlayClip(string name) //Начинает пригрывать аудио
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
    public void StopClip(string name) // Останавливет аудио
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
    public void StopClipWithDelay(string name) // Останавливает аудио послетенно уменьшая звук
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

    private IEnumerator stopWithDelay(Audio au)//Корутина для метода выше
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
