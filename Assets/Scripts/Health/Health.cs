using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Range(0, 6)]
    public int health;
    public int maxHealth;

    public void TakeHit(int damage)
    {
        health -= damage;

        if (health <= 0)
            SceneManager.LoadScene("RestartMenu");  
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

        if (health <= 0)
            SceneManager.LoadScene("RestartMenu");
    }
    public void SetBonusHealth(int NewMaxHealth, int NewHealth)
    {
        Debug.Log("New Health");
        maxHealth += NewMaxHealth;
        health += NewHealth;

        if(health > maxHealth)
            health = maxHealth;
    }
}