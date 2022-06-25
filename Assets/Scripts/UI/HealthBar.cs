using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text text;

    private void Awake() => FindObjectOfType<PlayerHealth>().onHealthChange.AddListener(SetHealth);
    public void SetHealth(int newHealth, int newMaxHealth)
    {
        slider.value = newHealth;
        slider.maxValue = newMaxHealth;
        text.text = newHealth + "/" + newMaxHealth;
    }
}
