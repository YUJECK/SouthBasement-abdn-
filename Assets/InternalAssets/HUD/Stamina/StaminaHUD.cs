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
        private void Construct(CharacterStats characterStats)
        {
            characterStats.Stamina.OnChanged +=
                stamine => _stamineScale.fillAmount = (float)stamine / characterStats.MaximumStamina;
        }
        
        private void Awake()
        {
            _stamineScale = GetComponent<Image>();
        }
    }
}