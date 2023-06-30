using System.Collections.Generic;
using SouthBasement.InternalAssets.Infrastructure;
using UnityEngine;

namespace SouthBasement.Helpers
{
    public static class ChanceSystem
    {
        public static int GetRandomChance()
            => Random.Range(0, 101);
        public static Rarity GetRandomRarity()
        {
            var chance = Random.Range(0, 101);

            return chance switch
            {
                >= (int) Rarity.B => Rarity.A,
                >= (int) Rarity.C => Rarity.B,
                >= (int) Rarity.D => Rarity.C,
                _ => Rarity.D
            };
        }

        public static bool FitsInChance(int chance, int requireChance)
            => chance >= requireChance;

        public static TWithChance[] GetInChance<TWithChance>(TWithChance[] withChances, int chance)
            where TWithChance : IWithChance
        {
            List<TWithChance> result = new(withChances);

            foreach (var withChance in withChances)
            {
                if(FitsInChance(withChance.SpawnChance, chance))
                    result.Add(withChance);
            }

            return result.ToArray();
        }
    }
}