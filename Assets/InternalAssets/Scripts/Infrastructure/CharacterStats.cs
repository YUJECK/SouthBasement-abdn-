using System;
using UnityEngine;

namespace TheRat
{
    public sealed class CharacterStats
    {
        public ObservableVariable<int> Damage { get; private set; } = new(35);
        public ObservableVariable<float> AttackRange { get; private set; } = new(0.4f);
        public ObservableVariable<float> AttackRate { get; private set; } = new(1f);
        public ObservableVariable<float> MoveSpeed { get; private set; } = new(5f);

        public int MaximumHealth { get; private set; } = 60;
        public int CurrentHealth { get; private set; } = 60;

        public Action<int> OnHealthChanged;
        
        public void SetHealth(int currentHealth)
        {
            CurrentHealth = currentHealth;

            if (CurrentHealth >= MaximumHealth)
                CurrentHealth = MaximumHealth;
            if(CurrentHealth <= 0)
                return;
                //делаем чтото
                
            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}