using System;

public interface IDamagable
{
    int CurrentHealth { get; }

    event Action<int> OnDamaged;
    
    void Damage(int damage);
}
