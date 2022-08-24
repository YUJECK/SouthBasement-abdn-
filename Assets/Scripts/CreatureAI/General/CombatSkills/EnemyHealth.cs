using Creature;
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
[AddComponentMenu("Creature/General/CombatSkills/Enemy Health")]
public class EnemyHealth : Health
{
    [Header("Выпадаемые предметы")]
    [SerializeField] private int minCheese = 2;
    [SerializeField] private int maxCheese = 9;
    [SerializeField] private List<Drop> itemsDrop = new List<Drop>();

    //Другое
    [Header("Другое")]
    private Coroutine damageInd;

    private void DropItem()
    {
        List<Drop> itemsInChance = new List<Drop>();
        if (itemsDrop.Count > 0)
        {
            int chance = PlayerStats.GenerateChance();

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
        currentHealth += heal;
        if (currentHealth >= maxHealth) currentHealth = maxHealth;

        onHealthChange.Invoke(currentHealth, maxHealth);
        onHeal.Invoke(currentHealth, maxHealth);
    }
    public override void TakeHit(int damage, float stunDuration = 0f)
    {
        currentHealth -= damage;
        if (damageInd != null) StopCoroutine(damageInd);
        
        onHealthChange.Invoke(currentHealth, maxHealth);
        onTakeHit.Invoke(currentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            onDie.Invoke();
            int cheese = Random.Range(minCheese, maxCheese);
        }
    }
    public override void SetHealth(int newMaxHealth, int newHealth)
    {
        maxHealth = newMaxHealth;
        Utility.CheckNumber(ref currentHealth, maxHealth, maxHealth, Utility.CheckNumberVariants.Much);
        
        newHealth -= currentHealth;
        if (newHealth < 0)
            TakeHit(-newHealth);
        else if (newHealth > 0)
            Heal(newHealth);
    }

    //Юнитивские методы
    private void Awake()
    {
        effectHandler = GetComponent<EffectHandler>();
        effectHandler.health = this;
        //stun.AddListener(GetComponent<EnemyAI>().GetStunned);
        onDie.AddListener(DropItem);
    }
}