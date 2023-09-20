using System;
using SouthBasement.Items;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters
{
    [RequireComponent(typeof(EffectsHandler))]
    public sealed class CharacterHealth : MonoBehaviour, IDamagable
    {
        private CharacterStats _healthStats;

        public int CurrentHealth => _healthStats.HealthStats.CurrentHealth;
        public EffectsHandler EffectsHandler { get; private set; }
        public event Action<int> OnDamaged;

        [Inject]
        private void Construct(CharacterStats characterStats)
        {
            _healthStats = characterStats;
        }

        private void Awake()
        {
            EffectsHandler = GetComponent<EffectsHandler>();
        }

        public void Damage(int damage, AttackTags[] args)
        {
            _healthStats.HealthStats.SetHealth(_healthStats.HealthStats.CurrentHealth - damage);
            OnDamaged?.Invoke(CurrentHealth);
        }
    }
}