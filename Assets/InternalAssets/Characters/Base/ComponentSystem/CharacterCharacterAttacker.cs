using System;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;

namespace SouthBasement.Characters.Components
{
    public abstract class CharacterCharacterAttacker<TOwner> : CharacterComponent<TOwner>, ICharacterAttacker
    {
        public event Action<IDamagable[]> OnAttacked;

        public abstract WeaponItem Weapon { get; }
        public bool Blocked { get; set; }

        protected void InvokeAttack(IDamagable[] hits) => OnAttacked?.Invoke(hits);
        public abstract AttackResult Attack();
        public abstract AttackResult DefaultAttack();
    }
}