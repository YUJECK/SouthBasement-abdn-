using System;
using UnityEngine;

namespace SouthBasement.Weapons
{
    [Serializable]
    public sealed class WeaponStats
    {
        [field: SerializeField] public int Damage { get; private set; } = 12;
        [field: SerializeField] public float AttackRange { get; private set; } = 0.4f;
        [field: SerializeField] public float AttackRate { get; private set; } = 1f;
        [field: SerializeField] public int StaminaRequire { get; private set; } = 10;
    }
}