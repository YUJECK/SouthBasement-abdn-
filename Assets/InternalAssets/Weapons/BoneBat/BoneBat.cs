using System;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace TheRat
{
    [CreateAssetMenu]
    public sealed class BoneBat : WeaponItem
    {
        public override Type GetItemType()
            => typeof(WeaponItem);
    }
}
