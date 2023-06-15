using System;

namespace TheRat.InventorySystem
{
    public abstract class ActiveItem : Item
    {
        public event Action OnUsed;

        public abstract void Use();
    }
}