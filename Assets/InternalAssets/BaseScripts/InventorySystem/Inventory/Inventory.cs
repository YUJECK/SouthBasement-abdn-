using System;
using Zenject;

namespace SouthBasement.InventorySystem
{
    public sealed class Inventory
    {
        public int ActiveItemsSlots { get; private set; } = 2;
        public int WeaponItemsSlots { get; private set; } = 3;

        private DiContainer _diContainer;
        public ItemsDictionaryContainer MainContainer;
        
        public event Action<Item> OnAdded;
        public event Action<string> OnRemoved;

        public Inventory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            MainContainer = new ItemsDictionaryContainer();

            MainContainer
                .AddContainer<JunkItem>(new StackableInventoryContainer(), 12)
                .AddContainer<FoodItem>(new InventoryContainer(), 6)
                .AddContainer<ActiveItem>(new InventoryContainer(), 2)
                .AddContainer<PassiveItem>(new InventoryContainer(), 24);

            MainContainer.AddContainer<WeaponItem>(new InventoryContainer(), 3)
                .AddSubContainerTo<WeaponItem>("bone_made")
                .AddSubContainerTo<WeaponItem>("wooden_made");
        }

        public bool TryAddItem(Item item)
        {
            if (MainContainer.TryAddItem(item, item.ItemCategory))
            {
                OnAdded?.Invoke(item);
                return true;
            }

            return false;
        }

        public void RemoveItem(string id)
        {
            if(MainContainer.Remove(id))
                OnRemoved?.Invoke(id);
        }
    }
}