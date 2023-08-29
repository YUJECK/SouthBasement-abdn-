using System;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement.BaseScripts.Tests.Kylysh
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Weapon + "Kylysh")]
    public sealed class Kylysh : WeaponItem
    {
        public override Type GetItemType()
            => typeof(WeaponItem);
    }
}