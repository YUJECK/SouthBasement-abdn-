using System;
using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement.Characters.Stats
{
    [Serializable]
    public sealed class CharacterCombatStats
    {
        [field: SerializeField] public CombatStats DefaultStats { get; private set; } = new();

         public CombatStats CurrentStats { get; private set; } = new CombatStats(); 

         public void SetWeapon(CombatStats weaponItem)
         {
             if (weaponItem != null)
                 CurrentStats = weaponItem;
         }
     }
}