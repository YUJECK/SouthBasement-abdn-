using System;
using TheRat.InputServices;

namespace TheRat.InventorySystem
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
            inventory.OnAddedActiveItem.OnAdded += SetCurrent;
        }
        ~ActiveItemUsage()
        {
            _inventory.OnAddedActiveItem.OnAdded -= SetCurrent;
        }

        public void SetCurrent(ActiveItem item)
        {
            _activeItem = item;
            OnSelected?.Invoke(item);
        }
    }
}