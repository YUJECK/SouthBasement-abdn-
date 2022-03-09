using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public int health;
    public int maxHealth;
    GameManager cheese;

    [SerializeField] int minCheese = 0;
    [SerializeField] int maxCheese = 5;
    public GameObject enemy;
    public RoomCloser roomCloser;

    private void Start()
    {
        cheese = FindObjectOfType<GameManager>();
    }
    public void TakeHit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            int cheeseCount = Random.Range(minCheese,maxCheese);
            Debug.Log("CheeseInEnemy" + cheeseCount);
            cheese.SpawnCheese(enemy, cheeseCount);
            Destroy(gameObject);
        }         
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
}