using System;
using UnityEngine;
using Zenject;

namespace TheRat.InventorySystem
{
    [AddComponentMenu("HUD/Inventory/InventoryHUD")]
    public sealed class InventoryHUD : MonoBehaviour
    {
        private InventorySlot[] _slots;

        [Inject]
        private void Construct(Inventory inventory)
        {
            inventory.OnAdded += OnAdded;
            inventory.OnRemoved += OnRemoved;
        }

        private void Awake()
        {
            _slots = GetComponentsInChildren<InventorySlot>();
        }

        private void OnAdded(Item item)
        {
            GetEmpty()?.SetItem(item);
        }

        private void OnRemoved(Item item)
        {
            Find(item)?.SetItem(null);
        }
        
        private InventorySlot Find(Item item)
        {
            foreach (var slot in _slots)
            {
                if (slot.CurrentItem.ItemID == item.ItemID)
                    return slot;
            }

            return null;
        }

        private InventorySlot GetEmpty()
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