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
        private CharacterStaminaStats _staminaStats;

        [Inject]
        private void Construct(CharacterStaminaStats staminaStats) => _staminaStats = staminaStats;
        private void Awake() => _stamineScale = GetComponent<Image>();

        private void OnEnable() => _staminaStats.Stamina.OnChanged += UpdateStamina;
        private void OnDisable() => _staminaStats.Stamina.OnChanged -= UpdateStamina;
 
        private void UpdateStamina(float stamine) => _stamineScale.fillAmount = (float)stamine / _staminaStats.MaximumStamina.Value;
    }
}