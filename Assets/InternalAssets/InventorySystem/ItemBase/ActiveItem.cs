using System;

namespace SouthBasement.InternalAssets.InventorySystem.ItemBase
{
    public abstract class ActiveItem : Item
    {
        public event Action OnUsed;

        public abstract void Use();
    }
}