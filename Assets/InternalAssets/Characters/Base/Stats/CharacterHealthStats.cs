using System;
using UnityEngine;

namespace SouthBasement.Characters.Stats
{
    [Serializable]
    public sealed class CharacterHealthStats
    {
        [field: SerializeField] public int MaximumHealth { get; private set; } = 60;
        [field: SerializeField] public int CurrentHealth { get; private set; } = 60;

        public event Action<int> OnHealthChanged;
        public event Action<int> OnMaximumHealthChanged;
        
        public event Action OnDied;
        
        public void SetHealth(int currentHealth)
            => SetHealth(currentHealth, MaximumHealth);

        public void SetHealth(int currentHealth, int maximumHealth)
        {
            CurrentHealth = currentHealth;

            if(maximumHealth != MaximumHealth)
                OnMaximumHealthChanged?.Invoke(maximumHealth);
            
            MaximumHealth = maximumHealth;

            if (CurrentHealth >= MaximumHealth)
                CurrentHealth = MaximumHealth;

            if (CurrentHealth <= 0)
                OnDied?.Invoke();
                
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}