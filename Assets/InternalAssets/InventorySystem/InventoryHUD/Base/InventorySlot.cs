using System;
using SouthBasement.InventoryHUD.Base;
using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement.InventorySystem
{
    public abstract class InventorySlot<TItem> : MonoBehaviour, IInventorySlot where TItem : Item 
    {
        [SerializeField] protected Image ItemImage;

        public Item CurrentItemNonGeneric => CurrentItem;
        public TItem CurrentItem { get; protected set; }

        public event Action<TItem> OnSetted;
        
        protected void DefaultStart()
        {
            if(CurrentItem == null)
                SetItem(null);
        }

        public virtual void SetItem(TItem item)
        {
            if (item == null)
            {
                ItemImage.color = Color.clear;
            }
            else
            {
                ItemImage.sprite = item.ItemSprite;
                ItemImage.color = Color.white;
            }

            CurrentItem = item;
            OnSetted?.Invoke(CurrentItem);
        }

        protected void InvokeOnSetted(TItem item)
        {
            OnSetted?.Invoke(item);
        }
    }
}