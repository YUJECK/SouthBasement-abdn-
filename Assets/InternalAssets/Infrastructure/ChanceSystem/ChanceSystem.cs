using System.Collections.Generic;
using SouthBasement;
using UnityEngine;

namespace SouthBasement.Helpers
{
    public static class ChanceSystem
    {
        public static int GetRandomChance()
            => Random.Range(0, 101);
        
        public static Rarity GetRandomRarity()
        {
            var chance = GetRandomChance();

            //TODO: Вернуть появление A редкости
            
            return chance switch
            {
                >= (int) Rarity.B => Rarity.C,
                >= (int) Rarity.C => Rarity.C,
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