using System;
using UnityEngine;

namespace TheRat.AI
{
    public abstract class EnemyHealth : MonoBehaviour, IDamagable
    {
        public int CurrentHealth { get; private set; } = 70;
        public event Action<int> OnDamaged;

        protected Enemy Enemy;
        
        public virtual void Damage(int damage)
        {
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