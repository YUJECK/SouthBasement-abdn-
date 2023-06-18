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
                .AddContainer<JunkItem>()
                .AddContainer<FoodItem>()
                .AddContainer<ActiveItem>()
                .AddContainer<PassiveItem>();

            MainContainer.AddContainer<WeaponItem>()
                .AddSubContainerTo<WeaponItem>("bone_made")
                .AddSubContainerTo<WeaponItem>("wooden_made");
        }

        public void AddItem(Item item)
        {
            if(MainContainer.TryAddItem(item, item.ItemCategory))
                OnAdded?.Invoke(item);
        }

        public void RemoveItem(string id)
        {
            if(MainContainer.Remove(id))
                OnRemoved?.Invoke(id);
        }
    }
}