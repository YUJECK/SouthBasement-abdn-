using System;

namespace SouthBasement.InternalAssets.InventorySystem.ItemBase
{
    public class JunkItem : Item
    {
        public override string GetStatsDescription()
        {
            return "That's just a junk";
        }

        public override Type GetItemType()
        {
            return typeof(JunkItem);
        }
    }
}