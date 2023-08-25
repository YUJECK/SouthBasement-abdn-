using System;
using SouthBasement;
using SouthBasement.Items;

public interface IDamagable
{
    int CurrentHealth { get; }
    EffectsHandler EffectsHandler { get; }

    event Action<int> OnDamaged;
    void Damage(int damage, AttackTags[] args);

}
