using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TheRat.HUD
{
    [AddComponentMenu("HUD/HealthBar")]
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text healthScore;
        [SerializeField] private Image barFill;
        private CharacterStats _characterStats;

        [Inject]
        private void Construct(CharacterStats characterStats)
        {
            _characterStats = characterStats;
            characterStats.OnHealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            barFill.fillAmount = (float)health / _characterStats.MaximumHealth;
            healthScore.text = $"{health} / {_characterStats.MaximumHealth}";
        }
    }
}