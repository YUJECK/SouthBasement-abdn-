using UnityEngine;
using Zenject;

namespace TheRat.InventorySystem
{
    [AddComponentMenu("HUD/Inventory/ActiveItemsInventoryHUD")]
    public sealed class ActiveItemsInventoryHUD : MonoBehaviour
    {
        private ActiveItemSlot[] _slots;

        [Inject]
        private void Construct(Inventory inventory)
        {
            inventory.OnAddedActiveItem += OnAddedActiveItem;
            inventory.OnRemoved += OnRemoved;
        }

        private void Awake()
        {
            _slots = GetComponentsInChildren<ActiveItemSlot>();
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