using System;
using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement.InventorySystem
{
    public abstract class InventorySlot<TItem> : MonoBehaviour where TItem : Item 
    {
        protected Image ItemImage;
        public TItem CurrentItem { get; protected set; }

        public event Action<TItem> OnSetted;
        
        protected void DefaultAwake()
        {
            ItemImage = GetComponent<Image>();
            SetItem(null);
        }
        
        public virtual void SetItem(TItem item)
        {
            if(item == null)
                ItemImage.color = Color.clear;
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