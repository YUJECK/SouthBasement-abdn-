using System;
using System.Globalization;
using NTC.GlobalStateMachine;
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
        
        public bool SetHealth(int currentHealth)
            => SetHealth(currentHealth, MaximumHealth);

        public bool SetHealth(int currentHealth, int maximumHealth)
        {
            CurrentHealth = currentHealth;

            if(maximumHealth != MaximumHealth)
                OnMaximumHealthChanged?.Invoke(maximumHealth);
            
            MaximumHealth = maximumHealth;

            if (CurrentHealth >= MaximumHealth)
                CurrentHealth = MaximumHealth;

            if (CurrentHealth <= 0)
            {
                GlobalStateMachine.Push<DiedState>();
                OnDied?.Invoke();
                return true;
            }
                
            OnHealthChanged?.Invoke(CurrentHealth);
            return false;
        }
    }
}