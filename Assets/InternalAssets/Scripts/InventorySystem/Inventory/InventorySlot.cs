using UnityEngine;
using UnityEngine.UI;

namespace TheRat.InventorySystem
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("HUD/Inventory/InventorySlot")]
    public sealed class InventorySlot : MonoBehaviour
    {
        private Image _itemImage;
        public Item CurrentItem { get; private set; }

        private void Awake()
        {
            _itemImage = GetComponent<Image>();
            SetItem(null);
        }

        public void SetItem(Item item)
        {
            if(item == null)
                _itemImage.color = Color.clear;
            else
            {
                _itemImage.sprite = item.ItemSprite;
                _itemImage.color = Color.white;
            }

            CurrentItem = item;
        }
    }
}