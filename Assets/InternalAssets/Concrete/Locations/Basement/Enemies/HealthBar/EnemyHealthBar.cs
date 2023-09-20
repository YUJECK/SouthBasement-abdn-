using SouthBasement.AI;
using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private EnemyHealth _enemyHealth;
        private int _startingHealth;
        private Image _image;
        
        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
            _enemyHealth = GetComponentInParent<EnemyHealth>();
            
            _startingHealth = _enemyHealth.CurrentHealth;
        }

        private void OnEnable()
            => _enemyHealth.OnDamaged += UpdateBar;
        private void OnDisable()
            => _enemyHealth.OnDamaged -= UpdateBar;

        private void UpdateBar(int health)
        {
            float fillAmount = (float) health / _startingHealth;
            fillAmount = Mathf.Clamp(fillAmount, 0.1f, 1f);
            
            _image.fillAmount = fillAmount;
        }
        }
}
