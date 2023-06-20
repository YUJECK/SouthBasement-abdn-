using SouthBasement.Characters;
using TheRat.Characters.Stats;
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
        
        private CharacterHealthStats _characterStats;

        [Inject]
        private void Construct(CharacterHealthStats characterStats)
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