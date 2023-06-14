using UnityEngine;

namespace TheRat.InventorySystem
{
    public abstract class WeaponItem : Item
    {
        [field: SerializeField] public int Damage { get; protected set; }
        
        public abstract void OnAttack();
    }
}