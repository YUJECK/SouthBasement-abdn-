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
    [SerializeField] private SpriteRenderer effectIndicator;

    [SerializeField] int minCheese = 0;
    [SerializeField] int maxCheese = 5;
    public GameObject enemy;
    public RoomCloser roomCloser;

    //Время действия еффектов
    private float burnTime;
    private float poisonedTime;
    private float bleedTime;

    //Время в которое еффект начал действовать
    private float burnStartTime;
    private float poisonedStartTime;
    private float bleedStartTime;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        effectsManager = FindObjectOfType<EffectsManager>();
    }
    public void TakeHit(int damage)
    {
        health -= damage;
        StartCoroutine(TakeHitVizualization());

        if (health <= 0)
        {
            int cheeseCount = Random.Range(minCheese,maxCheese);
            Debug.Log("CheeseInEnemy" + cheeseCount);
            gameManager.SpawnCheese(enemy.transform, cheeseCount);
            Destroy(gameObject);
        }         
    }
    private IEnumerator TakeHitVizualization()
    {
        gameObject.GetComponent<SpriteRenderer>().color= new Color(255,0,0,100);
        yield return new WaitForSeconds(0.6f);
        gameObject.GetComponent<SpriteRenderer>().color= new Color(100,100,100,100);
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
    public void GetBurn(float effectTime) { effectsManager.Burn.AddListener(Burn); burnTime = effectTime; burnStartTime = Time.time;}
    public void GetPoisoned(float effectTime) { effectsManager.Poisoned.AddListener(Poisoned); poisonedTime = effectTime; poisonedStartTime = Time.time;}
    public void GetBleed(float effectTime) { effectsManager.Bleed.AddListener(Bleed); bleedTime = effectTime; bleedStartTime = Time.time;}

    public void ResetBurn() {Debug.Log("reset"); effectsManager.Burn.RemoveListener(Burn); burnTime = 0f; burnStartTime = 0f;effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetPoisoned() { effectsManager.Poisoned.RemoveListener(Poisoned); poisonedTime = 0f; poisonedStartTime = 0f;effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetBleed() { effectsManager.Bleed.RemoveListener(Bleed); bleedTime = 0f; bleedStartTime = 0f; effectIndicator.sprite = gameManager.hollowSprite;}

    private void Burn()
    {
        effectIndicator.sprite = gameManager.BurnIndicator;
        TakeHit(1);
    }
    private void Poisoned()
    {
        effectIndicator.sprite = gameManager.PoisonedIndicator;
        TakeHit(1);
    }
    private void Bleed()
    {
        effectIndicator.sprite = gameManager.BleedIndicator;
        TakeHit(1);
    }

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