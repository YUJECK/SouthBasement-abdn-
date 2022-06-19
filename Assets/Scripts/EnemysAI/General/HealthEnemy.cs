using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable] public struct Effect{
    public float startTime;
    public float durationTime;
};
public class HealthEnemy : MonoBehaviour
{
    //Показатели здоровья
    public int health;
    public int maxHealth;

    public UnityEvent onDestroy = new UnityEvent();  //Методы которые вызовуться при уничтожении объекта
    public SpriteRenderer effectIndicator;
    [SerializeField] private Color damageColor;

    [SerializeField] int minCheese = 0;
    [SerializeField] int maxCheese = 4;
    [SerializeField] private string destroySound; // Звук смерти
    [SerializeField] private string hitSound; // Звук получения урона

    //Время действия еффектов
    public Effect burn;
    public Effect bleed;
    public Effect poisoned;
    [HideInInspector] public UnityEvent<float> stun = new UnityEvent<float>();
    public Effect regeneration;
    public Coroutine damageInd = null;

    //Ссылки на другие скрипты
    public RoomCloser roomCloser;
    private GameManager gameManager;
    [SerializeField] private AngryRatAI angryRatAi;
    private EffectsManager effectsManager;
    private AudioManager audioManager;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        effectsManager = FindObjectOfType<EffectsManager>();
        audioManager = FindObjectOfType<AudioManager>();
        
        if (roomCloser != null)
        { 
            roomCloser.EnemyCounterTunUp();
            onDestroy.AddListener(roomCloser.EnemyCounterTunDown);
        }
    }
    private void Update()
    {
        if (burn.durationTime != 0 && Time.time - burn.startTime > burn.durationTime) ResetBurn();
        if (bleed.durationTime != 0 && Time.time - bleed.startTime > bleed.durationTime) ResetBleed();
        if (poisoned.durationTime != 0 && Time.time - poisoned.startTime > poisoned.durationTime) { ResetPoisoned(); Debug.Log("pr"); }
        if (regeneration.durationTime != 0 && Time.time - regeneration.startTime > regeneration.durationTime) ResetRegeneration();
    }
    private void OnDestroy() { onDestroy.Invoke();  }
    
   
    //Всякие манипуляции со здоровьем
    public void TakeHit(int damage, float stunTime = 0f)
    {
        health -= damage;

        if (stunTime != 0f)
        {
            stun.Invoke(stunTime);
        }
        if (hitSound != "") audioManager.PlayClip(hitSound);
        if (damageInd != null) StopCoroutine(damageInd); 
            
        damageInd = StartCoroutine(TakeHitVizualization());

        if (health <= 0)
        {
            int cheeseCount = Random.Range(minCheese,maxCheese);
            Debug.Log("CheeseInEnemy" + cheeseCount);
            if(maxCheese != 0) gameManager.SpawnCheese(transform.position, cheeseCount);
            if(destroySound != "") audioManager.PlayClip(destroySound);
            Destroy(gameObject);
        }         
    }
    private IEnumerator TakeHitVizualization()
    {
        gameObject.GetComponent<SpriteRenderer>().color = damageColor;
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(100,100,100,100);
    }
    public void Heal(int bonusHealth)
    {
        health += bonusHealth;

        if(health > maxHealth)
            health = maxHealth;     
    }
    public void SetHealth(int NewMaxHealth, int NewHealth)
    {
        maxHealth = NewMaxHealth;
        health = NewHealth;
        
        if(health > maxHealth)
            health = maxHealth;
    }
    public void TakeAwayHealth(int TakeAwayMaxHealth, int TakeAwayHealth)
    {
        maxHealth -= TakeAwayMaxHealth;
        health -= TakeAwayHealth;
       
        if(health > maxHealth)
            health = maxHealth;
    }
    public void SetBonusHealth(int NewMaxHealth, int NewHealth)
    {
        Debug.Log("New Health");
        maxHealth += NewMaxHealth;
        health += NewHealth;

        if(health > maxHealth)
            health = maxHealth;
    }

    //Еффекты которые могут наложиться на врага    
    public void ResetBurn() { effectsManager.Burn.listeners.RemoveListener(Burn); burn.durationTime = 0f; burn.startTime = 0f;effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetPoisoned() { effectsManager.Poisoned.listeners.RemoveListener(Poisoned); poisoned.durationTime = 0f; poisoned.startTime = 0f;effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetBleed() { effectsManager.Bleed.listeners.RemoveListener(Bleed); bleed.durationTime = 0f; bleed.startTime = 0f; effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetRegeneration() { effectsManager.Regeneration.listeners.RemoveListener(Regeneration); regeneration.durationTime = 0f; regeneration.startTime = 0f; effectIndicator.sprite = gameManager.hollowSprite; } 

    public void Burn() { TakeHit(9); }
    public void Poisoned() { TakeHit(5); }
    public void Bleed() { TakeHit(14); }
    public void Regeneration() { Heal(10); }
}