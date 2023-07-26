using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int currentHealth = 60;
    [SerializeField] protected int maximumHealth = 60;

    public abstract void TakeHit(int damagePoints);
    public abstract void Heal(int healPoints);
    public abstract void DecreaseMaximumHealth(int damagePoints);
    public abstract void IncreaseMaximumHealth(int healPoints);
}