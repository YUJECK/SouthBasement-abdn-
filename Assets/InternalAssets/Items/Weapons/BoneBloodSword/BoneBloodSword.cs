using SouthBasement.InventorySystem;
using System;
using SouthBasement.Characters;
using SouthBasement.Characters.Components;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "BoneBloodSword")]
    public sealed class BoneBloodSword : WeaponItem
    {
        private Character _character;

        [Inject]
        private void Construct(Character character) 
            => _character = character;

        public override Type GetItemType()
            => typeof(WeaponItem);

        public override void OnAttack(IDamagable[] damagables)
        {
            foreach (var damagable in damagables)
                damagable.EffectsHandler.Add(new BleedEffect(2, 0.75f, 3f, damagable));
        }

        public override void OnEquip()
        {
            
        }

        public override void OnUnequip()
        {
            
        }
    }
}