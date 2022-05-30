using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios; // Все аудио в игре

    public List<Audio> nowPlaying = new List<Audio>(); //Все аудио который играют 
    private Audio _mainAudio = new Audio(); //Главная тема играющая сейчас 
    public Audio mainAudio
    {
        get { return _mainAudio; }
        private set { _mainAudio = value; }
    }

    private Audio _lastMain = null; //Главная тема игравшая раньше
    public Audio lastMain
    {
        get { return _lastMain; }
        private set { _lastMain = value; }
    }
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
        _mainAudio.source = gameObject.AddComponent<AudioSource>();
        
        _mainAudio.source.clip = _mainAudio.clip;
        _mainAudio.source.volume = _mainAudio.volume;
        _mainAudio.source.pitch = _mainAudio.pitch;
        _mainAudio.source.loop = _mainAudio.loop;
    }
 
    private void OnLevelWasLoaded()
    {
        //Останавливаем все аудио при переходе на след уровень
        foreach(Audio audio in audios)
        {
            if(audio.source.isPlaying)
                StopClipWithDelay(audio.name);
        }
        //Если есть какие то аудио который не играют, но остались в nowPlaying, то мы их убираем
        for(int i = 0; i < nowPlaying.Count; i++)
        {
            if(!nowPlaying[i].source.isPlaying)    
                nowPlaying.RemoveAt(i);
        }
    }
    public void SetToMain(string name = null, bool delay = false)
    {
        if(_mainAudio != null)
        {
            if(!delay) StopClip(null, _mainAudio);
            else StopClipWithDelay(name);
        } 

        _lastMain = _mainAudio;
        Audio au = Find(name);
        _mainAudio = au;
        _mainAudio.source = au.source;
        PlayClip(name);
    }

    public void PlayClip(string name = null) //Начинает пригрывать аудио
    {   
        Audio au = null;
        au = Find(name);

        if(au.source.isPlaying) Debug.Log(name);
        
        if(au != null && !au.source.isPlaying)
        {
            au.source.Play();
            if(au.loop) nowPlaying.Add(au);
        }
        else if(au != null)Debug.LogWarning("Clip " + au.name + " is playing");
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

        if(au != null && au.source.isPlaying)
        {
            au.source.Stop();
            nowPlaying.Remove(au);
        }
        else if(au != null)
            Debug.LogWarning("Audio " + name + " isn't plaing");
    }
    public void StopClipWithDelay(string name) // Останавливает аудио послетенно уменьшая звук
    {  
        Audio au = Find(name);
       
        if(au != null && au.source.isPlaying)
            StartCoroutine("stopWithDelay", au);
        else if(au != null)
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
        while(au.source.volume > 0)
        {
            yield return new WaitForSeconds(0.1f);
            au.source.volume -= 0.04f;
        }
        
        au.source.Stop();
        nowPlaying.Remove(au);
        au.source.volume = au.volume;
    }
}
