using System;
using UnityEngine;

namespace SouthBasement.Weapons
{
    [Serializable]
    public sealed class CombatStats
    {
        [field: SerializeField] public int Damage { get; set; } = 12;
        [field: SerializeField] public float AttackRange { get; set; } = 0.4f;
        [field: SerializeField] public float AttackRate { get; set; } = 1f;
        [field: SerializeField] public float StaminaRequire { get; set; } = 10;
    }
}