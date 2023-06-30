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
            int chance = Random.Range(0, 101);

            if (chance >= (int)Rarity.B)
                return Rarity.A;
            
            if (chance >= (int)Rarity.C)
                return Rarity.B;

            if (chance >= (int) Rarity.D)
                return Rarity.C;

            return Rarity.D;
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