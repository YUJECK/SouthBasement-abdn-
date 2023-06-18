using System;
using Zenject;

namespace SouthBasement.InventorySystem
{
    public sealed class Inventory : ITickable
    {
        public int ActiveItemsSlots { get; private set; } = 2;
        public int WeaponItemsSlots { get; private set; } = 3;

        private DiContainer _diContainer;
        private ItemsDictionaryContainer _itemsContainer;
        
        public event Action<Item> OnAdded;
        public event Action<string> OnRemoved;

        public Inventory(DiContainer diContainer)
        {
            _diContainer = diContainer;

            _itemsContainer = new ItemsDictionaryContainer();

            _itemsContainer
                .AddContainer<JunkItem>()
                .AddContainer<FoodItem>()
                .AddContainer<ActiveItem>()
                .AddContainer<PassiveItem>();

            _itemsContainer.AddContainer<WeaponItem>()
                .AddSubContainerTo<WeaponItem>("bone_made")
                .AddSubContainerTo<WeaponItem>("wooden_made");
        }

        public void AddItem(Item item)
        {
            if(_itemsContainer.TryAddItem(item, item.ItemCategory))
                OnAdded?.Invoke(item);
        }

        public void RemoveItem(string id)
        {
            if(_itemsContainer.Remove(id))
                OnRemoved?.Invoke(id);
        }

        public void Tick()
        {
            var passiveItems = _itemsContainer.GetAllInContainer<PassiveItem>();

            if (passiveItems == null) return;
            
            foreach (var item in passiveItems)
                item.OnRun();
        }
    }
}