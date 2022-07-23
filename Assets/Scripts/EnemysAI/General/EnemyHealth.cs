using EnemysAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Drop
{
    public GameObject drop;
    public int chance;
}
[RequireComponent(typeof(EffectHandler))]
public class EnemyHealth : Health
{
    [Header("Выпадаемые предметы")]
    [SerializeField] private int minCheese = 2;
    [SerializeField] private int maxCheese = 9;
    [SerializeField] private List<Drop> itemsDrop = new List<Drop>();

    //Другое
    [Header("Другое")]
    public RoomCloser roomCloser;
    private Coroutine damageInd;
    [HideInInspector] public AudioManager audioManager;

    private void DropItem()
    {
        List<Drop> itemsInChance = new List<Drop>();
        if (itemsDrop.Count > 0)
        {
            int chance = Random.Range(0, 101);
            chance -= PlayerStats.luck;

            foreach (Drop item in itemsDrop)
            {
                if (chance <= item.chance)
                    itemsInChance.Add(item);
            }
            Instantiate(itemsInChance[Random.Range(0, itemsInChance.Count)].drop, transform.position, Quaternion.identity);
        }
    }

    //Методы управления
    public override void Heal(int heal)
    {
        health += heal;
        if (health >= maxHealth) health = maxHealth;

        onHealthChange.Invoke(health, maxHealth);
    }
    public override void TakeHit(int damage, float stunDuration = 0f)
    {
        health -= damage;
        if (damageInd != null) StopCoroutine(damageInd);
        if (stunDuration > 0f)
            stun.Invoke(stunDuration);
        damageInd = StartCoroutine(TakeHitVizualization());
        onHealthChange.Invoke(health, maxHealth);

        if (health <= 0)
        {
            onDie.Invoke();
            int cheese = Random.Range(minCheese, maxCheese);
            if (destroySound != "") audioManager.PlayClip(destroySound);
            gameManager.SpawnCheese(gameObject.transform.position, cheese);
            if (destroyOnDie) Destroy(gameObject, destroyOffset);
        }
    }
    public override void SetHealth(int newMaxHealth, int newHealth)
    {
        maxHealth = newMaxHealth;
        if (health >= maxHealth) health = maxHealth;
        
        newHealth -= health;
        if (newHealth < 0)
            TakeHit(-newHealth);
        else if (newHealth > 0)
            Heal(newHealth);
    }

    //Юнитивские методы
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        effectManager = FindObjectOfType<EffectsInfo>();
        audioManager = FindObjectOfType<AudioManager>();
        if (roomCloser != null)
        {
            roomCloser.EnemyCounterTunUp();
            onDie.AddListener(roomCloser.EnemyCounterTunDown);
        }
        effectHandler = GetComponent<EffectHandler>();
        useEffects = true;
        effectHandler.health = this;
        stun.AddListener(GetComponent<EnemyAI>().GetStunned);
        onDie.AddListener(DropItem);
    }
}