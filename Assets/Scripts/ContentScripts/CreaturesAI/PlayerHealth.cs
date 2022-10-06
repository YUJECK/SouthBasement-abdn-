using UnityEngine;

public class PlayerHealth : Health
{
    public override void DecreaseMaximumHealth(int damagePoints)
    {
        maximumHealth -= damagePoints;

        if (currentHealth > maximumHealth)
            currentHealth = maximumHealth;
    }
    public override void Heal(int healPoints)
    {
        currentHealth += healPoints;

        if (currentHealth > maximumHealth)
            currentHealth = maximumHealth;
    }
    public override void IncreaseMaximumHealth(int healPoints)
    {
        maximumHealth += healPoints;
    }
    public override void TakeHit(int damagePoints)
    {
        currentHealth -= damagePoints;

        if (currentHealth < 0)
            Debug.Log(gameObject.name + " is dead");
    }
}