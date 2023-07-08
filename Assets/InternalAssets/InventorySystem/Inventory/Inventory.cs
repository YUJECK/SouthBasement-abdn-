using System;

namespace SouthBasement.InventorySystem
{
    public sealed class Inventory
    {
        public ItemsDictionaryContainer ItemsContainer { get; private set; }
        public event Action<Item> OnAdded;
        public event Action<string> OnRemoved;

        public Inventory()
            => ItemsContainer = new ItemsDictionaryContainer();

        public bool TryAddItem(Item item)
        {
            if (ItemsContainer.TryAddItem(item))
            {
                OnAdded?.Invoke(item);
                return true;
            }

            return false;
        }

        public void RemoveItem(string id)
        {
            if(ItemsContainer.Remove(id))
                OnRemoved?.Invoke(id);
        }
    }
}