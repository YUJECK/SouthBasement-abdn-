using UnityEngine;

public interface IDamagable
{
    int CurrentHealth { get; }

    void Damage(int damage);
}
