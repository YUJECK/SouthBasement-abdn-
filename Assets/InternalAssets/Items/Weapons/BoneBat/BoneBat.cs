using System;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement
{
    [CreateAssetMenu]
    public sealed class BoneBat : WeaponItem
    {
        public override Type GetItemType()
            => typeof(WeaponItem);
    }
}
