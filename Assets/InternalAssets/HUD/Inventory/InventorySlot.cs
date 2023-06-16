using System;
using UnityEngine;
using UnityEngine.UI;

namespace TheRat.InventorySystem
{
    public abstract class InventorySlot<TItem> : MonoBehaviour where TItem : Item 
    {
        protected Image ItemImage;
        public TItem CurrentItem { get; private set; }

        public event Action<TItem> OnSetted;
        
        public void SetItem(TItem item)
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
    }
}