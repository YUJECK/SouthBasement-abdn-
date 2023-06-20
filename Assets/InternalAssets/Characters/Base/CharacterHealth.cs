using System;
using TheRat.Characters.Stats;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters
{
    public sealed class CharacterHealth : MonoBehaviour, IDamagable
    {
        private CharacterHealthStats _healthStats;

        public int CurrentHealth => _healthStats.CurrentHealth;
        public event Action<int> OnDamaged;

        [Inject]
        private void Construct(CharacterHealthStats characterStats)
        {
            _healthStats = characterStats;
        }
        
        public void Damage(int damage)
        {
            _healthStats.SetHealth(_healthStats.CurrentHealth - damage);
            OnDamaged?.Invoke(CurrentHealth);
        }
    }
}