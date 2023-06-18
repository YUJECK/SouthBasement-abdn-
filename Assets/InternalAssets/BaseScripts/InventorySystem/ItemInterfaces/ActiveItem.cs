using System;

namespace SouthBasement.InventorySystem
{
    public abstract class ActiveItem : Item
    {
        public event Action OnUsed;

        public abstract void Use();
    }
}