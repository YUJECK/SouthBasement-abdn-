using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public abstract class WeaponItem : Item
    {
        [field: SerializeField] public WeaponStats WeaponStats { get; private set; }
        
        public abstract void OnAttack();
    }
}