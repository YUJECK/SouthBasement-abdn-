using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationHealth : Health
{
    public override void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth) health = maxHealth;
        onHealthChange.Invoke(maxHealth, health);
    }

    public override void SetHealth(int newMaxHealth, int newHealth)
    {
        health = newHealth;
        maxHealth = newMaxHealth;
        if (health > maxHealth) health = maxHealth;
        onHealthChange.Invoke(maxHealth, health);
    }

    public override void TakeHit(int damage, float stunDuration = 0)
    {
        health -= damage;
        if (health <= 0)
        {
            onHealthChange.Invoke(maxHealth, health);
            onDie.Invoke();
            Destroy(gameObject);
        }
    }
}
