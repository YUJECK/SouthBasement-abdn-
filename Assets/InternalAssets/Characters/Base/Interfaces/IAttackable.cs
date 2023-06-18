using System;
using SouthBasement.InventorySystem;

namespace SouthBasement
{
    public interface IAttackable : IDisposable
    {
        event Action<float> OnAttacked;
        
        WeaponItem Weapon { get; }
        
        void Attack();
    }
}