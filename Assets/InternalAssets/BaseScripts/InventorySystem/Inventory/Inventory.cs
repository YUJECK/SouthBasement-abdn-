using System;
using TheRat.HUD;
using Zenject;

namespace TheRat.InventorySystem
{
    public sealed class Inventory : ITickable
    {
        private DiContainer _container;
 
        public AddRemoveAction<JunkItem> OnAddedJunkItem = new();
        public AddRemoveAction<WeaponItem> OnAddedWeapon = new(); 
        public AddRemoveAction<PassiveItem> OnAddedPassive = new();
        public AddRemoveAction<ActiveItem> OnAddedActiveItem = new();
        
        private ItemsDictionaryContainer _itemsContainer;
        
        public int ActiveItemsSlots { get; private set; } = 2;
        public int WeaponItemsSlots { get; private set; } = 3;

        public Inventory(DiContainer container)
        {
            _container = container;

            _itemsContainer = new ItemsDictionaryContainer();
            
            _itemsContainer
                .AddContainer<Item>()
                .AddContainer<FoodItem>()
                .AddContainer<WeaponItem>()
                .AddContainer<ActiveItem>()
                .AddContainer<PassiveItem>();
        }

        public void AddItem(Item item)
            => _itemsContainer.TryAddItem(item, InvokeAddEvent);
        
        public void RemoveItem(string id)
            => _itemsContainer.RemoveItem(id, InvokeRemoveEvent);

        private void InvokeAddEvent(Type type, object item)
        {
            if(type == typeof(Item))
                OnAddedJunkItem?.InvokeAdded((JunkItem)item);
            if(type == typeof(ActiveItem))
                OnAddedActiveItem?.InvokeAdded((ActiveItem)item);
            if(type == typeof(WeaponItem))
                OnAddedWeapon?.InvokeAdded((WeaponItem)item);
            if(type == typeof(PassiveItem))
                OnAddedPassive?.InvokeAdded((PassiveItem)item);
        }
        private void InvokeRemoveEvent(Type type, object item)
        {
            if(type == typeof(Item))
                OnAddedJunkItem?.InvokeRemoved((JunkItem)item);
            if(type == typeof(ActiveItem))
                OnAddedActiveItem?.InvokeRemoved((ActiveItem)item);
            if(type == typeof(WeaponItem))
                OnAddedWeapon?.InvokeRemoved((WeaponItem)item);
            if(type == typeof(PassiveItem))
                OnAddedPassive?.InvokeRemoved((PassiveItem)item);
        }

        public void Tick()
        {
            var passiveItems = _itemsContainer.GetAllInContainer<PassiveItem>();

            if(passiveItems != null)
                foreach (var item in passiveItems)
                    item.OnRun();
        }
    }
}