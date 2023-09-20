using SouthBasement.InventorySystem.ItemBase;
using UnityEngine;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "BoneBloodSword")]
    public sealed class BoneBloodSword : WeaponItem
    {
        public override void OnAttack(AttackResult damaged)
        {
            foreach (var damagable in damaged.DamagedHits)
                damagable.EffectsHandler.Add(new BleedEffect(2, 0.75f, 3f, damagable));
        }

        public override void OnEquip()
        {
            
        }

        public override void OnTakeOff()
        {
            
        }
    }
}