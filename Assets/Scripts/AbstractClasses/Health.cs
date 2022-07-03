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
    /*[HideInInspector] */public float nextTime = 0f;
};
public abstract class Health : MonoBehaviour
{
    //Показатели здоровья
    [Header("Здоровье")]
    public int health;
    public int maxHealth;

    [SerializeField] private Color damageColor;
    public SpriteRenderer effectIndicator;

    [Header("Звуки")]
    [SerializeField] private string destroySound; // Звук смерти
    [SerializeField] private string hitSound; // Звук получения урона

    [Header("Еффекты")]
    public List<EffectsList> effectsCanUse;
    [HideInInspector] public EffectStats burn;
    /*[HideInInspector] */public EffectStats poison;
    [HideInInspector] public EffectStats bleed;
    [HideInInspector] public EffectStats regeneration;

    //События
    [Header("События")]
    public UnityEvent onDie = new UnityEvent();  //Методы которые вызовуться при уничтожении объекта
    public UnityEvent<int, int> onHealthChange = new UnityEvent<int, int>();
    public UnityEvent<float> stun = new UnityEvent<float>();
    public UnityEvent effects = new UnityEvent();

    //Другое
    private Coroutine damageInd = null;
    [HideInInspector] public EffectsInfo effectManager;
    public void DefaultOnDie() => Destroy(gameObject);

    //Всякие манипуляции со здоровьем
    public abstract void TakeHit(int damage, float stunDuration = 0f);
    public abstract void TakeAwayHealth(int takeAwayMaxHealth, int takeAwayHealth);
    public abstract void Heal(int heal);
    public abstract void SetHealth(int newMaxHealth, int newHealth);
    public abstract void PlusNewHealth(int newMaxHealth, int newHealth);

    public IEnumerator TakeHitVizualization()
    {
        gameObject.GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(100, 100, 100, 100);
    }

    //Еффекты которые могут наложиться на врага    
    private IEnumerator EffectActive(float duration, EffectStats effectStats, EffectsList effect)
    {
        if(effectsCanUse.Contains(effect))
        {
            UnityAction effectMethod = null;
            switch (effect)
            {
                case EffectsList.Burn:
                    effectIndicator.sprite = effectManager.burnIndicator;
                    burn = effectStats;
                    effects.AddListener(Burn);
                    effectMethod = Burn;
                    break;
                case EffectsList.Bleed:
                    effectIndicator.sprite = effectManager.bleedndicator;
                    bleed = effectStats;
                    effects.AddListener(Bleed);
                    effectMethod = Bleed;
                    break;
                case EffectsList.Poison:
                    effectIndicator.sprite = effectManager.poisonIndicator;
                    poison = effectStats;
                    effects.AddListener(Poison);
                    effectMethod = Poison;
                    Debug.Log("[Test]: Poisoned active on - " + gameObject.name);
                    break;
                case EffectsList.Regeneration:
                    effectIndicator.sprite = effectManager.regenerationIndicator;
                    regeneration = effectStats;
                    effects.AddListener(Regeneration);
                    effectMethod = Regeneration;
                    break;
            }
            yield return new WaitForSeconds(duration);
            effects.RemoveListener(effectMethod);
        }
    }
    public void GetEffect(float duration, EffectStats effectStats, EffectsList effect) => StartCoroutine(EffectActive(duration, effectStats, effect));

    //Еффекты
    public void Burn() 
    {
        if(burn.nextTime <= Time.time)
        {
            burn.nextTime = Time.time + burn.effectRate;
            TakeHit(burn.effectStrength); 
        }
    }
    public void Poison()
    {
        if (Time.time >= poison.nextTime)
        {
            Debug.Log("[Test]: Poison use on " + gameObject.name);
            poison.nextTime = Time.time + poison.effectRate;
            TakeHit(poison.effectStrength);
        }
        Debug.Log("[Test]: Poison hasn't been use on " + gameObject.name);
    }
    public void Bleed()
    {
        if (Time.time >= bleed.nextTime)
        {
            bleed.nextTime = Time.time + bleed.effectRate;
            TakeHit(bleed.effectStrength);
        }
    }
    public void Regeneration()
    {
        if (Time.time >= regeneration.nextTime)
        {
            regeneration.nextTime = Time.time + regeneration.effectRate;
            TakeHit(regeneration.effectStrength);
        }
    }
}