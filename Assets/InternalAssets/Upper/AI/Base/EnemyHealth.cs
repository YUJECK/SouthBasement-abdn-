using System;
using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(EffectsHandler))]
    public abstract class EnemyHealth : MonoBehaviour, IDamagable
    {
        [field: SerializeField] public int CurrentHealth { get; private set; } = 70;
        public EffectsHandler EffectsHandler { get; protected set; }
        public event Action<int> OnDamaged;

        protected Enemy Enemy;
        public virtual bool DefendedFromHits { get; set; }

        public virtual void Damage(int damage, AttackTags[] args)
        {
            if (DefendedFromHits) return;

                CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                Enemy.Die();
                Destroy(gameObject);
            }
            
            OnDamaged?.Invoke(CurrentHealth);
        }
    }
}