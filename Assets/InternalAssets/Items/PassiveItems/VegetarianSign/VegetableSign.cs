using System;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Items;
using SouthBasement.Items.Weapons;
using UnityEngine;

namespace SouthBasement.PassiveItems
{
    [CreateAssetMenu(menuName = AssetMenuHelper.PassiveItem + nameof(VegetableSign))]
    public class VegetableSign : PassiveItem
    {
        [SerializeField] private float damageMultiplier = 0.4f;

        public override Type GetItemType() 
            => typeof(PassiveItem);

        public override void OnAddedToInventory()
            => WeaponsStatsMultiplier.GetMultiplier(AttackTags.Vegetable).Damage += damageMultiplier;

        public override void OnRemovedFromInventory()
            => WeaponsStatsMultiplier.GetMultiplier(AttackTags.Vegetable).Damage -= damageMultiplier;
    }
}
    