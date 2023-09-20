using System.Collections.Generic;
using SouthBasement.Helpers;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement.Items.Weapons
{
    public static class WeaponsStatsMultiplier
    {
        private static readonly Dictionary<AttackTags, Multiplier> Multipliers = new();

        static WeaponsStatsMultiplier()
        {
            ResetAll();
        }

        public static void ResetAll()
        {
            Multipliers.Clear();
            var tags = EnumHelper.GetAllValues<AttackTags>();

            foreach (var tag in tags)
                Multipliers.Add(tag, new Multiplier());
        }

        public static CombatStats GetMultiplied(CombatStats stats)
        {
            CombatStats multipliedStats = new()
            {
                Damage = stats.Damage,
                AttackRate = stats.AttackRate,
                StaminaRequire = stats.StaminaRequire,
                AttackRange = stats.AttackRange,
                AttackTags = stats.AttackTags
            };

            foreach (var tag in multipliedStats.AttackTags)
            {
                var multiplier = GetMultiplier(tag);
                
                multipliedStats.Damage = (int)(multipliedStats.Damage * multiplier.Damage);
                multipliedStats.AttackRate = (int)(multipliedStats.AttackRate * multiplier.AttackRate);
                multipliedStats.StaminaRequire = (int)(multipliedStats.StaminaRequire * multiplier.StaminaRequire);
            }
            
            return multipliedStats;
        }

        public static CombatStats GetMultiplied(WeaponItem weaponItem)
            => GetMultiplied(weaponItem.OriginalCombatStats);

        public static Multiplier GetMultiplier(AttackTags tag)
        {
            return Multipliers[tag];
        }

        public sealed class Multiplier
        {
            private float _damage = 1;
            public float Damage
            {
                get => _damage;
                set
                {
                    value = Mathf.Clamp(value, 0.1f, 2f);
                    
                    _damage = value;
                }
            }
            
            private float _attackRate = 1;
            public float AttackRate
            {
                get => _attackRate;
                set
                {
                    value = Mathf.Clamp(value, 0.1f, 2f);
                    
                    _attackRate = value;
                }
            }
            
            private float _staminaRequire = 0;
            public float StaminaRequire
            {
                get => _staminaRequire;
                set
                {
                    value = Mathf.Clamp(value, 0.1f, 2f);
                    
                    _staminaRequire = value;
                }
            }
        }
    }
}