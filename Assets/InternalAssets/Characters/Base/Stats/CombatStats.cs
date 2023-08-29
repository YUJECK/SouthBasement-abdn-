using System.Collections.Generic;
using SouthBasement.Items;
using UnityEngine;
using System;
using SouthBasement.Items.Weapons;

namespace SouthBasement.Weapons
{
    [Serializable]
    public sealed class CombatStats
    {
        public CombatStats Multiplied => WeaponsStatsMultiplier.GetMultiplied(this);
        
        [field: SerializeField] public int Damage { get; set; } = 12;
        [field: SerializeField] public float AttackRange { get; set; } = 0.4f;
        [field: SerializeField] public float AttackRate { get; set; } = 1f;
        [field: SerializeField] public float StaminaRequire { get; set; } = 10;

        [field: SerializeField] public List<AttackTags> AttackTags { get; set; } = new();
    }
}