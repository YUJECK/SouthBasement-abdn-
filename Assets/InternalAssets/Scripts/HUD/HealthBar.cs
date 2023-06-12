using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TheRat.HUD
{
    [AddComponentMenu("HUD/HealthBar")]
    public sealed class HealthBar : MonoBehaviour
    {
        private Image _bar;
        private CharacterStats _characterStats;

        [Inject]
        private void Construct(CharacterStats characterStats)
        {
            _characterStats = characterStats;
            characterStats.OnHealthChanged += OnHealthChanged;
        }

        private void Awake()
        {
            _bar = GetComponent<Image>();
        }

        private void OnHealthChanged(int obj)
        {
            _bar.fillAmount = (float)obj / _characterStats.MaximumHealth;
        }
    }
}