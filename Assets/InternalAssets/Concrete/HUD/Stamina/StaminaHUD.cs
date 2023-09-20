using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.HUD
{
    [RequireComponent(typeof(Image))]
    public sealed class StaminaHUD : MonoBehaviour
    {
        private Image _stamineScale;
        private CharacterStats _staminaStats;

        [Inject]
        private void Construct(CharacterStats staminaStats) => _staminaStats = staminaStats;
        private void Awake() => _stamineScale = GetComponent<Image>();

        private void OnEnable() => _staminaStats.StaminaStats.Stamina.OnChanged += UpdateStamina;
        private void OnDisable() => _staminaStats.StaminaStats.Stamina.OnChanged -= UpdateStamina;
 
        private void UpdateStamina(float stamine) => _stamineScale.fillAmount = (float)stamine / _staminaStats.StaminaStats.MaximumStamina.Value;
    }
}