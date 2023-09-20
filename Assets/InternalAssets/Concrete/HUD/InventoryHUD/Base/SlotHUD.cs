using System;
using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement.HUD
{
    public abstract class SlotHUD<TSlot, TItem> : MonoBehaviour, ISlotHUD 
        where TSlot : InventorySlot<TItem> 
        where TItem : Item
    {
        protected TSlot[] Slots;

        protected void SetSlotsInChildren() 
            => Slots = GetComponentsInChildren<TSlot>(true);

        public Type GetTypeHUD()
        {
            return typeof(TItem);
        }

        public void UpdateInventory(Inventory inventory)
        {
            var items = inventory.ItemsContainer.GetAllInContainer<TItem>();

            foreach (var item in items)
            {
                Debug.Log(item.GetItemType().Name);
                GetEmpty()?.SetItem(item as TItem);
            }
        }

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
            foreach (var slot in Slots)
            {
                if (slot.CurrentItem.ItemID == item.ItemID)
                    return slot;
            }

            return null;
        }

        protected TSlot Find(string itemID)
        {
            foreach (var slot in Slots)
            {
                if (slot.CurrentItem != null && slot.CurrentItem.ItemID == itemID)
                    return slot;
            }

            return null;
        }

        
        protected TSlot GetEmpty()
        {
            foreach (var slot in Slots)
            {
                if (slot.CurrentItem == null)
                    return slot;
            }
            
            return null;
        }
    }
}