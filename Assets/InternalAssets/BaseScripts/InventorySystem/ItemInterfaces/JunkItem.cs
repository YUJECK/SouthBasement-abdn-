using System;

namespace SouthBasement.InventorySystem
{
    public class JunkItem : Item
    {
        public override Type GetItemType()
        {
            return typeof(JunkItem);
        }
    }
}