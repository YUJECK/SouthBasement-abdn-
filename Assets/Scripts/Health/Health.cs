using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Range(0, 6)]
    public int health;
    public int maxHealth;
    public Animator healthBar;
    private bool invisibleCadrs = false;
    public void TakeHit(int damage)
    {
        if(!invisibleCadrs)
        {
            health -= damage;

            invisibleCadrs = true;
            healthBar.SetBool("InvisibleCadrs",true);
            StartCoroutine(InvisibleCadrs());

            if (health <= 0)
                SceneManager.LoadScene("MainMenu");  
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

        if (health <= 0)
            SceneManager.LoadScene("MainMenu");
    }
    public void SetBonusHealth(int NewMaxHealth, int NewHealth)
    {
        Debug.Log("New Health");
        maxHealth += NewMaxHealth;
        health += NewHealth;

        if(health > maxHealth)
            health = maxHealth;
    }

    public IEnumerator InvisibleCadrs()
    {
        yield return new WaitForSeconds(1f);
        invisibleCadrs = false;
        healthBar.SetBool("InvisibleCadrs",false);
    }
}