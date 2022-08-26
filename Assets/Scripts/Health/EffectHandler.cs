using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] public class EffectStats
{
    public EffectStats(float rate, int strength)
    {
        effectRate = rate;
        effectStrength = strength;
    }
    public float effectRate;
    public int effectStrength;
    private float nextTime = 0f;
    public void SetNextTime(float rate) => nextTime = Time.time + rate;
    public float GetNextTime() { return nextTime; }
    public void ResetNextTimeToZero() => nextTime = 0f;
};
public class EffectHandler : MonoBehaviour
{
    [Header("Настройки еффектов")]
    [SerializeField] private List<EffectsList> effectsCanUse;
    public UnityEvent onStun = new UnityEvent();
    public UnityEvent onDisableStun = new UnityEvent();
    private EffectStats burn;
    private EffectStats poison;
    private EffectStats bleed;
    private EffectStats regeneration;

    [Header("Другое")]
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer firstEffectIndicator;
    [SerializeField] private SpriteRenderer secondEffectIndicator;
    private UnityEvent effects = new UnityEvent();
    private List<EffectsList> currentEffects = new List<EffectsList>();

    //Геттеры
    public Health Health => health;
    public bool CanUse(EffectsList effect) => effectsCanUse.Contains(effect); 
    public bool HasEffect(EffectsList effect) => currentEffects.Contains(effect);

    //Добавление, снятие еффектов
    private IEnumerator EffectActive(float duration, EffectStats effectStats, EffectsList effect)
    {
        if (effectsCanUse.Contains(effect))
        {
            SetEffect(effect, effectStats);
            yield return new WaitForSeconds(duration);
            RemoveEffect(effect);
        }
    }
    private void SetEffect(EffectsList effect, EffectStats effectStats)
    {
        //Если 2 эффекта, то мы убираем первый
        ref SpriteRenderer effectIndicator = ref firstEffectIndicator;
        if (currentEffects.Count == 1) effectIndicator = ref secondEffectIndicator;
        if (currentEffects.Count == 2) { RemoveEffect(currentEffects[0]); effectIndicator = firstEffectIndicator; }

        switch (effect)
        {
            case EffectsList.Burn:
                effectIndicator.sprite = ManagerList.EffectsInfo.burnIndicator;
                burn = effectStats;
                effects.AddListener(Burn);
                break;
            case EffectsList.Bleed:
                effectIndicator.sprite = ManagerList.EffectsInfo.bleedndicator;
                bleed = effectStats;
                effects.AddListener(Bleed);
                break;
            case EffectsList.Poison:
                effectIndicator.sprite = ManagerList.EffectsInfo.poisonIndicator;
                poison = effectStats;
                effects.AddListener(Poison);
                break;
            case EffectsList.Regeneration:
                effectIndicator.sprite = ManagerList.EffectsInfo.regenerationIndicator;
                regeneration = effectStats;
                effects.AddListener(Regeneration);
                break;
            case EffectsList.Stun:
                Stun(true);
                break;
        }
        currentEffects.Add(effect);
    }
    
    public void AddEffect(float duration, EffectStats effectStats, EffectsList newEffect) => StartCoroutine(EffectActive(duration, effectStats, newEffect));
    public void RemoveEffect(EffectsList effectsToRemove)
    {
        switch (effectsToRemove)
        {
            case EffectsList.None:
                return;
            case EffectsList.Burn:
                effects.RemoveListener(Burn);
                burn.ResetNextTimeToZero();
                break;
            case EffectsList.Bleed:
                effects.RemoveListener(Bleed);
                bleed.ResetNextTimeToZero();
                break;
            case EffectsList.Poison:
                effects.RemoveListener(Poison);
                poison.ResetNextTimeToZero();
                break;
            case EffectsList.Regeneration:
                effects.RemoveListener(Regeneration);
                regeneration.ResetNextTimeToZero();
                break;
            case EffectsList.Stun:
                Stun(false);
                break;
        }
        
        if(currentEffects.Count == 1) firstEffectIndicator.sprite = ManagerList.GameManager.hollowSprite;
        else secondEffectIndicator.sprite = ManagerList.GameManager.hollowSprite;
        
        currentEffects.Remove(effectsToRemove);
    }

    //Еффекты
    private void Burn()
    {
        if (Time.time >= burn.GetNextTime())
        {
            burn.SetNextTime(burn.effectRate);
            health.TakeHit(burn.effectStrength);
        }
    }
    private void Poison()
    {
        if (Time.time >= poison.GetNextTime())
        {
            poison.SetNextTime(poison.effectRate);
            health.TakeHit(poison.effectStrength);
        }
    }
    private void Bleed()
    {
        if (Time.time >= bleed.GetNextTime())
        {
            bleed.SetNextTime(bleed.effectRate);
            health.TakeHit(bleed.effectStrength);
        }
    }
    private void Regeneration()
    {
        if (Time.time >= regeneration.GetNextTime())
        {
            regeneration.SetNextTime(regeneration.effectRate);
            health.Heal(regeneration.effectStrength);
        }
    }
    private void Stun(bool active)
    {
        if(active) onStun.Invoke();
        else onDisableStun.Invoke();
    }

    //Работа еффектов
    private void Update() => effects.Invoke(); 
}