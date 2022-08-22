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
    public List<EffectsList> effectsCanUse;
    public UnityEvent onStun = new UnityEvent();
    public UnityEvent onDisableStun = new UnityEvent();
    private EffectStats burn;
    private EffectStats poison;
    private EffectStats bleed;
    private EffectStats regeneration;

    [Header("Другое")]
    public Health health;
    [SerializeField] private SpriteRenderer effectIndicator;
    [HideInInspector] public UnityEvent effects = new UnityEvent();
    private List<EffectsList> currentEffects = new List<EffectsList>();

    //Еффекты которые могут наложиться на врага    
    private IEnumerator EffectActive(float duration, EffectStats effectStats, EffectsList effect)
    {
        if (effectsCanUse.Contains(effect))
        {
            switch (effect)
            {
                case EffectsList.Burn:
                    //Добавление еффекта
                    effectIndicator.sprite = ManagerList.EffectsInfo.burnIndicator;
                    burn = effectStats;
                    effects.AddListener(Burn);

                    //Сброс еффекта
                    yield return new WaitForSeconds(duration);
                    effects.RemoveListener(Burn);
                    burn.ResetNextTimeToZero();
                    effectIndicator.sprite = ManagerList.GameManager.hollowSprite;
                    break;
                case EffectsList.Bleed:
                    //Добавление еффекта
                    effectIndicator.sprite = ManagerList.EffectsInfo.bleedndicator;
                    bleed = effectStats;
                    effects.AddListener(Bleed);

                    //Сброс еффекта
                    yield return new WaitForSeconds(duration);
                    effects.RemoveListener(Bleed);
                    bleed.ResetNextTimeToZero();
                    effectIndicator.sprite = ManagerList.GameManager.hollowSprite;
                    break;
                case EffectsList.Poison:
                    //Добавление еффекта
                    effectIndicator.sprite = ManagerList.EffectsInfo.poisonIndicator;
                    poison = effectStats;
                    effects.AddListener(Poison);

                    //Сброс еффекта
                    yield return new WaitForSeconds(duration);
                    effects.RemoveListener(Poison);
                    poison.ResetNextTimeToZero();
                    effectIndicator.sprite = ManagerList.GameManager.hollowSprite;
                    break;
                case EffectsList.Regeneration:
                    //Добавление еффекта
                    effectIndicator.sprite = ManagerList.EffectsInfo.regenerationIndicator;
                    regeneration = effectStats;
                    effects.AddListener(Regeneration);

                    //Сброс еффекта
                    yield return new WaitForSeconds(duration);
                    effects.RemoveListener(Regeneration);
                    regeneration.ResetNextTimeToZero();
                    effectIndicator.sprite = ManagerList.GameManager.hollowSprite;
                    break;
                case EffectsList.Stun:
                    Stun(true);
                    yield return new WaitForSeconds(duration);
                    Stun(false);
                    break;
            }
        }
    }
    public void GetEffect(float duration, EffectStats effectStats, EffectsList newEffect) => StartCoroutine(EffectActive(duration, effectStats, newEffect));
    public void ResetEffect(EffectsList effectsToRemove)
    {
        switch (effectsToRemove)
        {
            case EffectsList.None:
                return;
            case EffectsList.Burn:
                effects.RemoveListener(Burn);
                break;
            case EffectsList.Bleed:
                effects.RemoveListener(Bleed);
                break;
            case EffectsList.Poison:
                effects.RemoveListener(Poison);
                break;
            case EffectsList.Regeneration:
                effects.RemoveListener(Regeneration);
                break;
            case EffectsList.Stun:
                Stun(false);
                break;
        }
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

    private void Update() { effects.Invoke(); }
}