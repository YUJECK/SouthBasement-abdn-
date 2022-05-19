using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios; // Все аудио в игре

    public List<Audio> nowPlaying = new List<Audio>(); //Все аудио который играют 
    public Audio mainAudio = null; //Главная тема играющая сейчас 
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
        
        //"Активация" главной темы         
        mainAudio.source = gameObject.AddComponent<AudioSource>();
        
        mainAudio.source.clip = mainAudio.clip;
        mainAudio.source.volume = mainAudio.volume;
        mainAudio.source.pitch = mainAudio.pitch;
        mainAudio.source.loop = mainAudio.loop;
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
    public void SetToMain(string name = null, Audio audio = null, float time = 0f)
    {
        if(mainAudio != null) StopClip(null, mainAudio);
        
        if(audio == null)
        {
            Audio au = Find(name);
            mainAudio = au;
            mainAudio.source = au.source;
            PlayClip(name, null, time);
        }
        else
        {
            Audio au = Find(null, audio);
            mainAudio.source = au.source;
            mainAudio = au;
            PlayClip(null, audio, time);
        }
    }

    public void PlayClip(string name = null, Audio audio = null, float time = 0f) //Начинает пригрывать аудио
    {   
        Audio au = null;
        
        if(audio == null) au = Find(name);
        else au = audio;

        if(!au.source.isPlaying)
        {
            au.source.Play();
            nowPlaying.Add(au);
        }
    }
    public void StopAll() //Выключить все играющие сейчас аудио
    {
        foreach(Audio au in nowPlaying)
            au.source.Stop();
    }
    public void StopClip(string name = null, Audio audio = null) // Останавливет аудио
    {  
        Audio au = null;
        
        if(audio == null) au = Find(name);
        else au = audio;

        if(au.source.isPlaying)
        {
            au.source.Stop();
            nowPlaying.Remove(au);
        }
        else
            Debug.LogWarning("Audio " + name + " isn't plaing");
    }
    public void StopClipWithDelay(string name) // Останавливает аудио послетенно уменьшая звук
    {  
        Audio au = Find(name);
       
        if(au.source.isPlaying)
            StartCoroutine("stopWithDelay", au);
        else
            Debug.LogWarning("Audio " + name + " isn't plaing");
    }


    private Audio Find(string name = null, Audio audio = null)
    {
        Audio au = null;

        //Поиск
        for(int i = 0; i < audios.Length; i++)
        {
            if(audio == null)
                if(name == audios[i].name) {au = audios[i]; break;}
            else if(audio != null)
                if(audio.name == audios[i].name) {au = audios[i]; break;}
        }
        //Возврат
        if(au != null) return au;
        else 
        {
            Debug.LogWarning("Audio " + name + " not fount");
            return new Audio();
        }
    }
    private IEnumerator stopWithDelay(Audio au)//Корутина для метода выше
    {
        while(au.volume > 0)
        {
            yield return new WaitForSeconds(0.1f);
            au.source.volume -= 0.04f;
        }
        
        au.source.Stop();
        nowPlaying.Remove(au);
        au.source.volume = au.volume;
    }
}
