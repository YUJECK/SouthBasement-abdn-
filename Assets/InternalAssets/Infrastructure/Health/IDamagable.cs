using System;
using SouthBasement;

public interface IDamagable
{
    int CurrentHealth { get; }
    EffectsHandler EffectsHandler { get; }

    event Action<int> OnDamaged;
    
    void Damage(int damage, string[] args);
}
