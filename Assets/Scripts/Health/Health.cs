using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private struct Effect{
        public float startTime;
        public float durationTime;

    };
    public int health;
    public int maxHealth;
    public Animator healthBar;
    private AudioManager audioManager;
    private GameManager gameManager;
    private bool invisibleCadrs = false;

    //Еффекты
    public SpriteRenderer effectIndicator;
    private EffectsManager effectsManager;
    private Effect burn;
    private Effect bleed;
    private Effect poisoned;
    private Effect regeneration;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }


    public void TakeHit(int damage)
    {
        if(!invisibleCadrs)
        {
            health -= damage;
            audioManager.PlayClip("RatHurt");

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

    //Еффекты public void GetBurn(float effectTime) 
    
    public void ResetBurn() { effectsManager.Burn.listeners.RemoveListener(Burn); burn.durationTime = 0f; burn.startTime = 0f;effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetPoisoned() { effectsManager.Poisoned.listeners.RemoveListener(Poisoned); poisoned.durationTime = 0f; poisoned.startTime = 0f;effectIndicator.sprite = gameManager.hollowSprite;}
    public void ResetBleed() { effectsManager.Bleed.listeners.RemoveListener(Bleed); bleed.durationTime = 0f; bleed.startTime = 0f; effectIndicator.sprite = gameManager.hollowSprite;}

    public void Burn(int hit){TakeHit(hit);}
    public void Poisoned(int hit){TakeHit(hit);}
    public void Bleed(int hit){TakeHit(hit);}
    public void Regeneration(int regHealth){Heal(regHealth);}
}