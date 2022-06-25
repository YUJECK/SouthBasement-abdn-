using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectsManager : MonoBehaviour 
{
    [System.Serializable] public struct Effect{
        public float rate;
        public Sprite indicator;
        public UnityEvent listeners;
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
        if(Time.time >= Burn.nextTime)
        {
            Burn.listeners.Invoke();
            Burn.nextTime = Time.time + Burn.rate;
        }
        if(Time.time >= Poisoned.nextTime)
        {
            Poisoned.listeners.Invoke();
            Poisoned.nextTime = Time.time + Poisoned.rate;
        }
        if(Time.time >= Bleed.nextTime)
        {
            Bleed.listeners.Invoke();
            Bleed.nextTime = Time.time + Bleed.rate;
        }
        if(Time.time >= Regeneration.nextTime)
        {
            Regeneration.listeners.Invoke();
            Regeneration.nextTime = Time.time + Regeneration.rate;
        }
    }

    public void GetBleed(float durationTime, PlayerHealth plHealth = null, HealthEnemy enHealth = null)
    {
        if(plHealth != null) 
        {
            Bleed.listeners.AddListener(plHealth.Bleed);
            plHealth.effectIndicator.sprite = Bleed.indicator;
        }
        if(enHealth != null) 
        {
            enHealth.bleed.durationTime = durationTime;
            enHealth.bleed.startTime = Time.time;
            Bleed.listeners.AddListener(enHealth.Bleed);
            enHealth.effectIndicator.sprite = Bleed.indicator;
        }
    }
    public void GetBurn(float durationTime, PlayerHealth plHealth = null, HealthEnemy enHealth = null)
    {
        if(plHealth != null) 
        {
            Burn.listeners.AddListener(plHealth.Burn);
            plHealth.effectIndicator.sprite = Burn.indicator;
        }
        if(enHealth != null) 
        {
            enHealth.burn.durationTime = durationTime;
            enHealth.burn.startTime = Time.time;
            Burn.listeners.AddListener(enHealth.Burn);
            enHealth.effectIndicator.sprite = Burn.indicator;
        }
    }
    public void GetPoisoned(float durationTime, PlayerHealth plHealth = null, HealthEnemy enHealth = null)
    {
        if(plHealth != null) 
        {
            Poisoned.listeners.AddListener(plHealth.Poisoned);
            plHealth.effectIndicator.sprite = Poisoned.indicator;
        }
        if(enHealth != null)
        {
            enHealth.poisoned.durationTime = durationTime;
            enHealth.poisoned.startTime = Time.time;
            Poisoned.listeners.AddListener(enHealth.Poisoned);
            enHealth.effectIndicator.sprite = Poisoned.indicator;
        }
    }
    public void GetRegenerate(float durationTime, PlayerHealth plHealth = null, HealthEnemy enHealth = null)
    {
        if(plHealth != null) 
        {
            Regeneration.listeners.AddListener(plHealth.Regeneration);
            plHealth.effectIndicator.sprite = Regeneration.indicator;
        }
        if(enHealth != null) 
        {
            enHealth.regeneration.durationTime = durationTime;
            enHealth.regeneration.startTime = Time.time;
            Regeneration.listeners.AddListener(enHealth.Regeneration);
            enHealth.effectIndicator.sprite = Regeneration.indicator;
        }
    }
}
    
public enum EffectsList
{
    None,
    Burn,
    Bleed,
    Poisoned,
    Regeneration
}