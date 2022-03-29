using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffectsManager : MonoBehaviour 
{
    [SerializeField] private float burnRate = 0.5f;
    public UnityEvent Burn = new UnityEvent();
    private float nextTimeBurn;
    
    [SerializeField] private float poisonedRate = 0.5f;
    public UnityEvent Poisoned = new UnityEvent();
    private float nextTimePoisoned;
    
    [SerializeField] private float bleedRate = 0.3f;
    public UnityEvent Bleed = new UnityEvent();
    private float nextTimeBleed;


    private void FixedUpdate()
    {
        if(Time.time >= nextTimeBurn)
        {
            Burn.Invoke();
            nextTimeBurn = Time.time + 1f / burnRate;
        }
        if(Time.time >= nextTimePoisoned)
        {
            Poisoned.Invoke();
            nextTimePoisoned = Time.time + 1f / poisonedRate;
        }
        if(Time.time >= nextTimeBleed)
        {
            Bleed.Invoke();
            nextTimeBleed = Time.time + 1f / bleedRate;
        }
    }
}
