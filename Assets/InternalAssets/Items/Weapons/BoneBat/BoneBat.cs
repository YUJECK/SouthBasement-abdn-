using System;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "BoneBat")]
    public sealed class BoneBat : WeaponItem
    {
        public override Type GetItemType()
            => typeof(WeaponItem);
    }
}
