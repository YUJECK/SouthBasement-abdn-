using System;

namespace TheRat.Characters.Stats
{
    public sealed class CharacterHealthStats
    {
        public int MaximumHealth { get; private set; } = 60;
        public int CurrentHealth { get; private set; } = 60;

        public Action<int> OnHealthChanged;
        public Action<int> OnMaximumHealthChanged;
        
        public Action OnDied;
        
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