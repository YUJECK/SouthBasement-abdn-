using System;
using SouthBasement.InputServices;
using SouthBasement.InventorySystem.ItemBase;

namespace SouthBasement.InventorySystem
{
    public sealed class ActiveItemUsage 
    {
        private ActiveItem _activeItem;
        
        public event Action<ActiveItem> OnSelected;

        public Inventory _inventory;
        
        public ActiveItemUsage(IInputService service, Inventory inventory)
        {
            _inventory = inventory;
            
            service.ActiveItemUsage += () => _activeItem?.Use();
            
            inventory.OnAdded += SetCurrent;
            inventory.OnRemoved += OnRemoved;
        }

        private void OnRemoved(string id)
        {
            if(_activeItem == null)
                return;
            
            if (id == _activeItem.ItemID)
                _activeItem = null;
        }

        ~ActiveItemUsage()
        {
            _inventory.OnAdded -= SetCurrent;
            _inventory.OnRemoved += OnRemoved;
        }

        public void SetCurrent(Item item)
        {
            if (item.GetItemType() == typeof(ActiveItem))
            {
                _activeItem = item as ActiveItem;
                OnSelected?.Invoke(_activeItem);
            }
        }
    }
}