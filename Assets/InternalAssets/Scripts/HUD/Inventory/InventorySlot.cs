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
            GetComponentInParent<Button>().onClick.AddListener(TryUseItem);
            SetItem(null);
        }

        private void TryUseItem()
        {
            if(CurrentItem is IUsableItem usableItem)
                usableItem.Use();
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