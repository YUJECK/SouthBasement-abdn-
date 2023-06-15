using TheRat.Weapons;
using UnityEngine;

namespace TheRat.InventorySystem
{
    public abstract class WeaponItem : Item
    {
        [field: SerializeField] public WeaponStats WeaponStats { get; private set; }
        
        public abstract void OnAttack();
    }
}