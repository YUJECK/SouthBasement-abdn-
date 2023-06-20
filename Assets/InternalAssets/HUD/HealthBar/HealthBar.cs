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
        
        private CharacterHealthStats _healthStats;

        [Inject]
        private void Construct(CharacterHealthStats characterStats)
            => _healthStats = characterStats;

        private void Awake()
            => OnHealthChanged(_healthStats.CurrentHealth);

        private void OnEnable()
        {
            _healthStats.OnHealthChanged += OnHealthChanged;
            OnHealthChanged(_healthStats.CurrentHealth);
        }

        private void OnDisable()
            => _healthStats.OnHealthChanged -= OnHealthChanged;

        private void OnHealthChanged(int health)
        {
            barFill.fillAmount = (float)health / _healthStats.MaximumHealth;
            healthScore.text = $"{health} / {_healthStats.MaximumHealth}";
        }
    }
}