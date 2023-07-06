using System;
using SouthBasement.InventorySystem;

namespace SouthBasement.Characters.Components
{
    public abstract class CharacterAttacker<TOwner> : CharacterComponent<TOwner>, IAttacker
    {
        public event Action<IDamagable[]> OnAttacked;

        public abstract WeaponItem Weapon { get; }
        public bool Blocked { get; set; }

        protected void InvokeAttack(IDamagable[] hits) => OnAttacked?.Invoke(hits);
        public abstract void Attack();
    }
}