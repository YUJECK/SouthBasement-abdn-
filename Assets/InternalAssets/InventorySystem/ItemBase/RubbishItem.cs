using System;
using UnityEngine;

namespace SouthBasement.InventorySystem.ItemBase
{
    [CreateAssetMenu(menuName = AssetMenuHelper.Rubbish)]
    public class RubbishItem : Item
    {
        public override string GetStatsDescription()
        {
            return "That's just a rubbish";
        }

        public override Type GetItemType()
        {
            return typeof(RubbishItem);
        }
    }
}