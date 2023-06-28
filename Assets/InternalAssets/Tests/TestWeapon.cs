using System;
using NaughtyAttributes.Test;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement
{
    [CreateAssetMenu]
    public class TestWeapon : WeaponItem
    {
        public override void OnAttack(IDamagable[] hitted)
        {
            
        }

        public override Type GetItemType()
        {
            return typeof(WeaponItem);
        }
    }
}
