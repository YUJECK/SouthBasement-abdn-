using System;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Items;
using SouthBasement.Items.Weapons;
using UnityEngine;

namespace SouthBasement.BaseScripts.Tests
{
    [CreateAssetMenu(menuName = AssetMenuHelper.PassiveItem + "SteelHorseNecklace")]
    public sealed class SteelHorseNecklace : PassiveItem
    {
        public override Type GetItemType() => typeof(PassiveItem);
        
        public override void OnAddedToInventory()
            => WeaponsStatsMultiplier.GetMultiplier(AttackTags.Metal).Damage += 0.35f;

        public override void OnRemovedFromInventory()
            => WeaponsStatsMultiplier.GetMultiplier(AttackTags.Metal).Damage -= 0.35f;
    }
}