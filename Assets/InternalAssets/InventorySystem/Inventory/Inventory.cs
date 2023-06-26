using System;
using Zenject;

namespace SouthBasement.InventorySystem
{
    public sealed class Inventory
    {
        private DiContainer _diContainer;
        public ItemsDictionaryContainer ItemsContainer;
        
        public event Action<Item> OnAdded;
        public event Action<string> OnRemoved;

        public Inventory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            ItemsContainer = new ItemsDictionaryContainer();

            ItemsContainer
                .AddContainer<JunkItem>(new StackableTypeContainer(), 12)
                .AddContainer<FoodItem>(new TypeContainer(), 6)
                .AddContainer<ActiveItem>(new TypeContainer(), 2)
                .AddContainer<PassiveItem>(new TypeContainer(), 24)
                .AddContainer<WeaponItem>(new TypeContainer(), 3);
        }

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