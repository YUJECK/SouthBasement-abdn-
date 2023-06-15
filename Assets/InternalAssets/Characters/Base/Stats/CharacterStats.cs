using System;
using TheRat.Weapons;

namespace TheRat
{
    [Serializable]
    public sealed class CharacterStats
    {
        public WeaponStats WeaponStats { get; set; } = new();
        public ObservableVariable<float> MoveSpeed { get; private set; } = new(5f);

        public int MaximumStamina { get; set; } = 100;
        public ObservableVariable<int> Stamina { get; set; } = new(100);
        public float StaminaIncreaseRate { get; set; } = 0.1f;
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