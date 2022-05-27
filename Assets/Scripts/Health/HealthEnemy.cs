using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    //Показатели здоровья
    public int health;
    public int maxHealth;

    private GameManager gameManager;
    private EffectsManager effectsManager;
    private AudioManager audioManager;
    public SpriteRenderer effectIndicator;
    [SerializeField] private Color damageColor;

    [SerializeField] int minCheese = 0;
    [SerializeField] int maxCheese = 5;
    [SerializeField] private string destroySound; // Звук смерти
    [SerializeField] private string hitSound; // Звук получения урона
    public RoomCloser roomCloser;

    //Время действия еффектов
    private float burnTime;
    private float poisonedTime;
    private float bleedTime;

    //Время в которое еффект начал действовать
    private float burnStartTime;
    private float poisonedStartTime;
    private float bleedStartTime;
    
    private Coroutine damageInd = null;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        effectsManager = FindObjectOfType<EffectsManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    public void TakeHit(int damage)
    {
        health -= damage;
        if(hitSound != "")audioManager.PlayClip(hitSound);
        
        if(damageInd != null)
            StopCoroutine(damageInd); 
            
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

    private void OnDestroy()
    {
        if (roomCloser != null)
            roomCloser.enemyesCount--;
    }

    //Еффекты которые могут наложиться на врага
    public void GetBurn(float effectTime) 
    { 
        effectsManager.Burn.listeners.AddListener(Burn); 
        burnTime = effectTime; 
        burnStartTime = Time.time;
        effectIndicator.sprite = gameManager.BurnIndicator;
    }
    public void GetPoisoned(float effectTime) 
    { 
        effectsManager.Poisoned.listeners.AddListener(Poisoned); 
        poisonedTime = effectTime; 
        poisonedStartTime = Time.time;
        effectIndicator.sprite = gameManager.PoisonedIndicator;    
    }
    public void GetBleed(float effectTime) 
    {
        effectsManager.Bleed.listeners.AddListener(Bleed);
        bleedTime = effectTime; 
        bleedStartTime = Time.time;
        effectIndicator.sprite = gameManager.BleedIndicator;
    }

    public void ResetBurn() {effectsManager.Burn.listeners.RemoveListener(Burn); burnTime = 0f; burnStartTime = 0f;effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetPoisoned() { effectsManager.Poisoned.listeners.RemoveListener(Poisoned); poisonedTime = 0f; poisonedStartTime = 0f;effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetBleed() { effectsManager.Bleed.listeners.RemoveListener(Bleed); bleedTime = 0f; bleedStartTime = 0f; effectIndicator.sprite = gameManager.hollowSprite;}

    public void Burn(int hit){TakeHit(hit);}
    public void Poisoned(int hit){TakeHit(hit);}
    public void Bleed(int hit){TakeHit(hit);}
    public void Regeneration(int regHealth){Heal(regHealth);}

    private void Update()
    {
        //Сброс еффекта если его время закончилось
        if(Time.time - burnStartTime >= burnTime & burnTime != 0f)
        {
            ResetBurn();
            Debug.Log(Time.time - burnStartTime + " >= " + burnTime);
        }

        if(Time.time - poisonedStartTime >= poisonedTime & poisonedTime != 0f)
            ResetPoisoned();

        if(Time.time - bleedStartTime >= bleedTime & bleedTime != 0f)
            ResetBleed();
    }
}