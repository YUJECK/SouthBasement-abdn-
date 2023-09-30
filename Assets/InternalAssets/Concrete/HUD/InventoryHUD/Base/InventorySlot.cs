using System;
using SouthBasement.InventorySystem.ItemBase;
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

        private void OnDisable()
        {
            CurrentItem.OnItemSpriteChanged -= UpdateSprite;
        }

        public virtual void SetItem(TItem item)
        {
            if (item == null)
            {
                ItemImage.color = Color.clear;
                
                if(CurrentItem != null)
                    CurrentItem.OnItemSpriteChanged -= UpdateSprite;
            }
            else
            {
                UpdateSprite(item.ItemSprite);
                ItemImage.color = Color.white;
                item.OnItemSpriteChanged += UpdateSprite;
            }

            CurrentItem = item;
            OnSetted?.Invoke(CurrentItem);
        }

        private void UpdateSprite(Sprite sprite)
            => ItemImage.sprite = sprite;

        protected void InvokeOnSetted(TItem item)
        {
            OnSetted?.Invoke(item);
        }
    }
}