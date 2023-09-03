using System;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;

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
                item.OnAddedToInventory();
                OnAdded?.Invoke(item);
                return true;
            }

            return false;
        }

        public void RemoveItem(string id)
        {
            var container = ItemsContainer.FindWithItem(id);
            container.TryGetItem(id, out Item item);

            if (ItemsContainer.Remove(id))
            {
                if(item != null)
                    item.OnRemovedFromInventory();

                OnRemoved?.Invoke(id);
            }
        }
    }
}