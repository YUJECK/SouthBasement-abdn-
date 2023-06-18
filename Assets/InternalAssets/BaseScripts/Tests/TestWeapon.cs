using System;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement
{
    [CreateAssetMenu]
    public class TestWeapon : WeaponItem
    {
        public override void OnAttack()
        {
            
        }

        public override Type GetItemType()
        {
            return typeof(WeaponItem);
        }
    }
}
