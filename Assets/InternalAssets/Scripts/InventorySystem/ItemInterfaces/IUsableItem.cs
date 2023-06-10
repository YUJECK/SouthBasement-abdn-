using System;

namespace TheRat.InventorySystem
{
    public interface IUsableItem
    {
        event Action OnUsed;

        void Use();
    }
}