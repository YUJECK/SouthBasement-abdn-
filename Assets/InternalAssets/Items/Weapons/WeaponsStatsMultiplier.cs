using System.Collections.Generic;
using SouthBasement.Helpers;
using SouthBasement.InventorySystem;
using SouthBasement.Weapons;

namespace SouthBasement.Items.Weapons
{
    public static class WeaponsStatsMultiplier
    {
        private static readonly Dictionary<ItemsTags, Multiplier> Multipliers = new();

        static WeaponsStatsMultiplier()
        {
            var tags = EnumHelper.GetAllValues<ItemsTags>();

            foreach (var tag in tags)
                Multipliers.Add(tag, new Multiplier());
        }

        public static CombatStats GetMultiplied(WeaponItem weaponItem)
        {
            CombatStats multipliedStats = new();
            List<Multiplier> multipliers = new();

            foreach (var tag in weaponItem.ItemTags)
            {
                var multiplier = GetMultiplier(tag);
                
                multipliers.Add(multiplier);

                multipliedStats.Damage = (int)(multipliedStats.Damage * multiplier.Damage);
                multipliedStats.AttackRate = (int)(multipliedStats.AttackRate * multiplier.AttackRate);
                multipliedStats.StaminaRequire = (int)(multipliedStats.StaminaRequire * multiplier.StaminaRequire);
            }

            return multipliedStats;
        }
        
        public static Multiplier GetMultiplier(ItemsTags tag)
        {
            return Multipliers[tag];
        }

        public sealed class Multiplier
        {
            public float Damage = 1;
            public float AttackRate = 1;
            public float StaminaRequire = 1;
        }
    }
}