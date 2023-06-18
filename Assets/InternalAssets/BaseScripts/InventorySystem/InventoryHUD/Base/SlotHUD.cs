using System;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement.HUD
{
    public class SlotHUD<TSlot, TItem> : MonoBehaviour 
        where TSlot : InventorySlot<TItem> 
        where TItem : Item
    {
        protected TSlot[] _slots;

        protected void SetSlotsInChildren() 
            => _slots = GetComponentsInChildren<TSlot>();

        protected virtual void OnAdded(Item item)
        {
            if(item is TItem itemToAdd)
                GetEmpty()?.SetItem(itemToAdd);
        }

        protected virtual void OnRemoved(string itemID)
        {
            Find(itemID)?.SetItem(null);
        }
        
        protected TSlot Find(TItem item)
        {
            foreach (var slot in _slots)
            {
                if (slot.CurrentItem.ItemID == item.ItemID)
                    return slot;
            }

            return null;
        }

        protected TSlot Find(string itemID)
        {
            foreach (var slot in _slots)
            {
                if (slot.CurrentItem != null && slot.CurrentItem.ItemID == itemID)
                    return slot;
            }

            return null;
        }

        
        protected TSlot GetEmpty()
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