using System;
using SouthBasement.Characters.Stats;
using SouthBasement.Items;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters
{
    [RequireComponent(typeof(EffectsHandler))]
    public sealed class CharacterHealth : MonoBehaviour, IDamagable
    {
        private CharacterHealthStats _healthStats;

        public int CurrentHealth => _healthStats.CurrentHealth;
        public EffectsHandler EffectsHandler { get; private set; }
        public event Action<int> OnDamaged;

        [Inject]
        private void Construct(CharacterHealthStats characterStats)
        {
            _healthStats = characterStats;
        }

        private void Awake()
        {
            EffectsHandler = GetComponent<EffectsHandler>();
        }

        public void Damage(int damage, AttackTags[] args)
        {
            _healthStats.SetHealth(_healthStats.CurrentHealth - damage);
            OnDamaged?.Invoke(CurrentHealth);
        }
    }
}