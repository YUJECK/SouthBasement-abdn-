using System;
using System.Collections;
using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.HUD
{
    [AddComponentMenu("HUD/HealthBar")]
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthScore;
        [SerializeField] private Image barFill;
        
        private CharacterStats _healthStats;

        [Inject]
        private void Construct(CharacterStats characterStats)
            => _healthStats = characterStats;

        private void Awake()
            => OnHealthChanged(_healthStats.HealthStats.CurrentHealth);

        private void OnEnable()
        {
            _healthStats.HealthStats.OnHealthChanged += OnHealthChanged;
            OnHealthChanged(_healthStats.HealthStats.CurrentHealth);
        }

        private void OnDisable()
            => _healthStats.HealthStats.OnHealthChanged -= OnHealthChanged;

        private void OnHealthChanged(int health)
        {
            barFill.fillAmount = (float)health / _healthStats.HealthStats.MaximumHealth;
            healthScore.text = $"{health} / {_healthStats.HealthStats.MaximumHealth}";
        }
    }
}