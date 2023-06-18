using System;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters
{
    public sealed class CharacterHealth : MonoBehaviour, IDamagable
    {
        private CharacterStats _characterStats;

        public int CurrentHealth => _characterStats.CurrentHealth;
        public event Action<int> OnDamaged;

        [Inject]
        private void Construct(CharacterStats characterStats)
        {
            _characterStats = characterStats;
        }
        
        public void Damage(int damage)
        {
            _characterStats.SetHealth(_characterStats.CurrentHealth - damage);
            OnDamaged?.Invoke(CurrentHealth);
        }
    }
}