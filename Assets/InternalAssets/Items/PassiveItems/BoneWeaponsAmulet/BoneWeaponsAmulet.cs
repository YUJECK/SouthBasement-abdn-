using SouthBasement.Characters.Components;
using SouthBasement.InventorySystem;
using SouthBasement.Characters;
using UnityEngine;
using System;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Items;
using SouthBasement.Items.Weapons;
using Zenject;

namespace SouthBasement.BaseScripts.Tests
{
    [CreateAssetMenu(menuName = AssetMenuHelper.PassiveItem + "BoneWeaponsAmulet")]
    public sealed class BoneWeaponsAmulet : PassiveItem
    {
        [SerializeField] private float mulptiplier = 0.2f;

        public override void OnAddedToInventory()
            => WeaponsStatsMultiplier.GetMultiplier(AttackTags.Bone).Damage += mulptiplier;

        public override void OnRemovedFromInventory()
            => WeaponsStatsMultiplier.GetMultiplier(AttackTags.Bone).Damage -= mulptiplier;
        
        public override string GetStatsDescription()
        {
            return "Multiply bone weapons damage on 1.2 units";
        }
    }
}