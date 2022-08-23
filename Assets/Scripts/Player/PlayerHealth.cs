using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(EffectHandler))]
public class PlayerHealth : Health
{
    [Header("Другое")]
    private bool invisibleCadrs = false;
    public Animator healthBar;
    
    //Методы управления
    public override void Heal(int heal)
    {
        currentHealth += heal;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        
        onHealthChange.Invoke(currentHealth, maxHealth);
        onHeal.Invoke(currentHealth, maxHealth);
    }
    public override void SetHealth(int newMaxHealth, int newHealth)
    {
        maxHealth = newMaxHealth;
        if (CurrentHealth >= MaxHealth) currentHealth = MaxHealth;

        newHealth -= CurrentHealth;
        if (newHealth < 0)
            TakeHit(-newHealth);
        else if (newHealth > 0)
            Heal(newHealth);
    }
    public override void TakeHit(int damage, float stunDuration = 0)
    {
        if (!invisibleCadrs)
        {
            currentHealth -= damage;

            invisibleCadrs = true;
            healthBar.SetBool("InvisibleCadrs", true);
            StartCoroutine(InvisibleCadrs());

            onHealthChange.Invoke(currentHealth, maxHealth);
            onTakeHit.Invoke(currentHealth, maxHealth);

            if (CurrentHealth <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene("RestartMenu");
            }
        }
    }
    
    //Юнитивские методы
    private void Start()
    {
        onHealthChange.Invoke(CurrentHealth, MaxHealth);
        effectHandler = GetComponent<EffectHandler>();
        effectHandler.health = this;
    }
    public IEnumerator InvisibleCadrs()
    {
        yield return new WaitForSeconds(1f);
        invisibleCadrs = false;
        healthBar.SetBool("InvisibleCadrs", false);
    }
}