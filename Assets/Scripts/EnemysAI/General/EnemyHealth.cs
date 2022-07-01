using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class Drop
{
    public GameObject drop;
    public int chance;
}
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
    [HideInInspector] public GameManager gameManager;
    [HideInInspector] public AudioManager audioManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        onDie.AddListener(DefaultOnDie);
        if (roomCloser != null)
        {
            roomCloser.EnemyCounterTunUp();
            onDie.AddListener(roomCloser.EnemyCounterTunDown);
        }
        onDie.AddListener(DropItem);
    }
    public void Update() { effects.Invoke(); }

    private void DropItem()
    {
        List<Drop> itemsInChance = new List<Drop>();
        if (itemsDrop.Count > 0)
        {
            int chance = Random.Range(0, 101);
            chance -= gameManager.luck;

            foreach (Drop item in itemsDrop)
            {
                if (chance <= item.chance)
                    itemsInChance.Add(item);
            }
            Instantiate(itemsInChance[Random.Range(0, itemsInChance.Count)].drop, transform.position, Quaternion.identity);
        }
    }

    public override void Heal(int heal)
    {
        health += heal;
        if (health >= maxHealth) health = maxHealth;

        onHealthChange.Invoke(health, maxHealth);
    }
    public override void PlusNewHealth(int newMaxHealth, int newHealth)
    {
        maxHealth += newMaxHealth;
        health += newHealth;

        if (health > maxHealth)
            health = maxHealth;
        onHealthChange.Invoke(health, maxHealth);
    }
    public override void SetHealth(int newMaxHealth, int newHealth)
    {
        maxHealth = newMaxHealth;
        health = newMaxHealth;

        if (health >= maxHealth) health = maxHealth;
        if (health < 0) onDie.Invoke();
        onHealthChange.Invoke(health, maxHealth);
    }
    public override void TakeAwayHealth(int takeAwayMaxHealth, int takeAwayHealth)
    {
        health -= takeAwayHealth;
        maxHealth -= takeAwayMaxHealth;

        if (health >= maxHealth) health = maxHealth;
        if (health < 0) onDie.Invoke();
        onHealthChange.Invoke(health, maxHealth);
    }
    public override void TakeHit(int damage, float stunDuration )
    {
        health -= damage;
        if (damageInd != null) StopCoroutine(damageInd);
        if (stunDuration > 0f) stun.Invoke(stunDuration);
        damageInd = StartCoroutine(TakeHitVizualization());
        onHealthChange.Invoke(health, maxHealth);

        if (health <= 0)
        {
            onDie.Invoke();
            int cheese = Random.Range(minCheese, maxCheese);
            Debug.Log(cheese);
            gameManager.SpawnCheese(gameObject.transform.position, cheese);
        }
    }
}
