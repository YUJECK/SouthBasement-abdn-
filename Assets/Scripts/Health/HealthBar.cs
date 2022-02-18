using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health playerHealth;
    public Slider slider;

    private void Start()
    {
        SetMaxHealth(playerHealth.maxHealth);
        SetHealth(playerHealth.health);
    }
    private void Update()
    {
        SetMaxHealth(playerHealth.maxHealth);
        SetHealth(playerHealth.health);
    }

    public void SetMaxHealth(int NewMaxHealth)
    {
        slider.maxValue = NewMaxHealth;
    }
    public void SetHealth(int NewHealth)
    {
        slider.value = NewHealth;
    }
}
