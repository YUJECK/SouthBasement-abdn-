using System.Collections.Generic;
using SouthBasement.InternalAssets.Infrastructure;
using UnityEngine;

namespace SouthBasement.Helpers
{
    public static class ChanceSystem
    {
        public static int GetChance()
            => UnityEngine.Random.Range(0, 101);

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