using TheRat.InventorySystem;
using UnityEngine;

namespace TheRat.HUD
{
    public class SlotHUD<TSlot, TItem> : MonoBehaviour 
        where TSlot : InventorySlot<TItem> 
        where TItem : Item
    {
        protected TSlot[] _slots;

        protected void SetSlotsInChildren() 
            => _slots = GetComponentsInChildren<TSlot>();

        protected virtual void OnAdded(TItem item)
        {
            GetEmpty()?.SetItem(item);
        }

        protected virtual void OnRemoved(TItem item)
        {
            Find(item)?.SetItem(null);
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

        protected  TSlot GetEmpty()
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