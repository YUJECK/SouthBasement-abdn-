using System;

namespace SouthBasement.InventorySystem.ItemBase
{
    public abstract class ActiveItem : Item
    {
        public event Action OnUsed;

        public override Type GetItemType()
            => typeof(ActiveItem);

        public abstract void Use();
    }
}