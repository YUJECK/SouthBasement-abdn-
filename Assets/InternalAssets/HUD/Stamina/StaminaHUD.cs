using TheRat.Characters.Stats;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.HUD
{
    [RequireComponent(typeof(Image))]
    public sealed class StaminaHUD : MonoBehaviour
    {
        private Image _stamineScale;

        [Inject]
        private void Construct(CharacterStaminaStats staminaStats)
        {
            staminaStats.Stamina.OnChanged +=
                stamine => _stamineScale.fillAmount = (float)stamine / staminaStats.MaximumStamina.Value;
        }
        
        private void Awake()
        {
            _stamineScale = GetComponent<Image>();
        }
    }
}