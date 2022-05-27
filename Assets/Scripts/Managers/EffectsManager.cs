using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectsManager : MonoBehaviour 
{
    [System.Serializable] public struct Effect{
        public float rate;
        public UnityEvent<int> listeners;
        public float nextTime;
    }
    
    [Header("Список имеющихся еффектов")]
    public Effect Burn;
    public Effect Poisoned;
    public Effect Bleed;
    public Effect Regeneration;

    //Ссылки на др скрипты
    private GameManager gameManager;


    private void FixedUpdate()
    {
        // if(Time.time >= nextTimeBurn)
        // {
        //     Burn.Invoke();
        //     nextTimeBurn = Time.time + 1f / burnRate;
        // }
        // if(Time.time >= nextTimePoisoned)
        // {
        //     Poisoned.Invoke();
        //     nextTimePoisoned = Time.time + 1f / poisonedRate;
        // }
        // if(Time.time >= nextTimeBleed)
        // {
        //     Bleed.Invoke();
        //     nextTimeBleed = Time.time + 1f / bleedRate;
        // }
        // if(Time.time >= nextTimeRegeneration)
        // {
        //     Regeneration.Invoke();
        //     nextTimeRegeneration = Time.time + 1f / regenerationRate;
        // }
    }

    public void GetBleed(float durationTime, Health plHealth = null, HealthEnemy enHealth = null)
    {
        if(plHealth != null) 
        {
            Bleed.listeners.AddListener(plHealth.Bleed);
            plHealth.effectIndicator.sprite = gameManager.BleedIndicator;
        }
        if(enHealth != null) 
        {
            Bleed.listeners.AddListener(enHealth.Bleed);
            enHealth.effectIndicator.sprite = gameManager.BleedIndicator;
        }
    }
    public void GetBurn(float durationTime, Health plHealth = null, HealthEnemy enHealth = null)
    {
        if(plHealth != null) 
        {
            Burn.listeners.AddListener(plHealth.Burn);
            plHealth.effectIndicator.sprite = gameManager.BurnIndicator;
        }
        if(enHealth != null) 
        {
            Burn.listeners.AddListener(enHealth.Burn);
            enHealth.effectIndicator.sprite = gameManager.BurnIndicator;
        }
    }
    public void GetPoisoned(float durationTime, Health plHealth = null, HealthEnemy enHealth = null)
    {
        if(plHealth != null) 
        {
            Poisoned.listeners.AddListener(plHealth.Poisoned);
            plHealth.effectIndicator.sprite = gameManager.PoisonedIndicator;
        }
        if(enHealth != null) 
        {
            Poisoned.listeners.AddListener(enHealth.Poisoned);
            enHealth.effectIndicator.sprite = gameManager.PoisonedIndicator;
        }
    }
    public void GetRegenerate(float durationTime, Health plHealth = null, HealthEnemy enHealth = null)
    {
        if(plHealth != null) 
        {
            Regeneration.listeners.AddListener(plHealth.Regeneration);
            plHealth.effectIndicator.sprite = gameManager.RegenerateIndicator;
        }
        if(enHealth != null) 
        {
            Regeneration.listeners.AddListener(enHealth.Regeneration);
            enHealth.effectIndicator.sprite = gameManager.RegenerateIndicator;
        }
    }
}
