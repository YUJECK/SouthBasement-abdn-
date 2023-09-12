using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using TMPro;
using UnityEngine;

namespace SouthBasement.HUD
{
    [AddComponentMenu("HUD/Inventory/JunkItemSlot")]
    public class JunkItemSlot : InventorySlot<RubbishItem>
    {
        [SerializeField] private TMP_Text _itemsCount;
        public int CurrentJunkCount { get; private set; }

        private void Start()
            => DefaultStart();

        public void AddItem()
        {
            CurrentJunkCount++;
            _itemsCount.text = $"{CurrentJunkCount}";
        }

        public void RemoveItem()
        {
            CurrentJunkCount--;
            _itemsCount.text = $"{CurrentJunkCount}";
            
            if(CurrentJunkCount <= 0)
                SetItem(null);
        }
        public override void SetItem(RubbishItem item)
        {
            if (item == null)
            {
                ItemImage.color = Color.clear;
                _itemsCount.text = "";
            }
            else
            {
                ItemImage.sprite = item.ItemSprite;
                ItemImage.color = Color.white;
                
                CurrentJunkCount = 1;
                _itemsCount.text = $"{CurrentJunkCount}";
            }

            CurrentItem = item;
            InvokeOnSetted(CurrentItem);
        }
    }
}