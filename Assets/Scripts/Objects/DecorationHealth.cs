using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationHealth : Health
{
    public override void Heal(int heal)
    {
        currentHealth += heal;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        onHealthChange.Invoke(maxHealth, currentHealth);
    }

    public override void SetHealth(int newMaxHealth, int newHealth)
    {
        currentHealth = newHealth;
        maxHealth = newMaxHealth;
        if (CurrentHealth > maxHealth) currentHealth = maxHealth;
        onHealthChange.Invoke(maxHealth, currentHealth);
    }

    public override void TakeHit(int damage, float stunDuration = 0)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            onHealthChange.Invoke(maxHealth, currentHealth);
            onDie.Invoke();
            Destroy(gameObject);
        }
    }
}
