using System;
using SouthBasement;
using UnityEngine;

namespace SouthBasement.Characters.Stats
{
    [Serializable]
    public sealed class CharacterStaminaStats
    {
        [field: SerializeField] public ObservableVariable<int> MaximumStamina { get; set; } = new(100);
        [field: SerializeField] public float StaminaIncreaseRate { get; set; } = 0.1f;
        public ObservableVariable<float> Stamina { get; set; } = new(100);
    }
}