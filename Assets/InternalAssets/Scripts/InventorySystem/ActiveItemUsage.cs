using System;
using TheRat.InputServices;

namespace TheRat.InventorySystem
{
    public sealed class ActiveItemUsage 
    {
        private ActiveItem _activeItem;
        private ActiveItemUsage _activeItemUsage;

        public event Action<ActiveItem> OnSelected;
        
        public ActiveItemUsage(IInputService service, Inventory inventory)
        {
            service.ActiveItemUsage += () => _activeItem?.Use();
            inventory.OnAddedActiveItem += item => _activeItem = item;
        }

        public void SetCurrent(ActiveItem item)
        {
            _activeItem = item;
            OnSelected?.Invoke(item);
        }
    }
}