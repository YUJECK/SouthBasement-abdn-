using UnityEngine;
using Zenject;

namespace TheRat.InventorySystem
{
    [AddComponentMenu("HUD/Inventory/ActiveItemsInventoryHUD")]
    public sealed class ActiveItemsInventoryHUD : MonoBehaviour
    {
        private ActiveItemSlot[] _slots;
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
            
            inventory.OnAddedActiveItem.OnAdded += OnAddedActiveItem;
            inventory.OnAddedActiveItem.OnRemoved += OnRemoved;
        }

        private void Awake()
        {
            _slots = GetComponentsInChildren<ActiveItemSlot>();
        }

        private void OnDestroy()
        {
            _inventory.OnAddedActiveItem.OnAdded -= OnAddedActiveItem;
            _inventory.OnAddedActiveItem.OnRemoved -= OnRemoved;
        }

        private void OnAddedActiveItem(ActiveItem item)
        {
            GetEmpty()?.SetItem(item);
        }

        private void OnRemoved(Item item)
        {
            Find(item)?.SetItem(null);
        }
        
        private ActiveItemSlot Find(Item item)
        {
            foreach (var slot in _slots)
            {
                if (slot.CurrentItem.ItemID == item.ItemID)
                    return slot;
            }

            return null;
        }

        private ActiveItemSlot GetEmpty()
        {
            foreach (var slot in _slots)
            {
                if (slot.CurrentItem == null)
                    return slot;
            }
            
            return null;
        }
    }
}