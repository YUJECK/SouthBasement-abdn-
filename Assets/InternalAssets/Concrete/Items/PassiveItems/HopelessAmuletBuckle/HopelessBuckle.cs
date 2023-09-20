using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.Items;
using SouthBasement.Items.Weapons;
using UnityEngine;

namespace SouthBasement.BaseScripts.Tests.HopelessAmulet
{
    [CreateAssetMenu(menuName = AssetMenuHelper.PassiveItem + nameof(HopelessBuckle))]
    public sealed class HopelessBuckle : PassiveItem
    {
        [SerializeField] private float bonus;

        public override void OnAddedToInventory()
            => WeaponsStatsMultiplier.GetMultiplier(AttackTags.Magic).Damage += bonus;
        
        public override void OnRemovedFromInventory()
            => WeaponsStatsMultiplier.GetMultiplier(AttackTags.Magic).Damage -= bonus;
    }
}