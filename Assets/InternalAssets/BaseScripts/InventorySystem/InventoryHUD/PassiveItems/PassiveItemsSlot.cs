using SouthBasement.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement.HUD
{
    [RequireComponent(typeof(Image))]
    public sealed class PassiveItemsSlot : InventorySlot<PassiveItem>
    {
        private void Awake()
        {
            ItemImage = GetComponent<Image>();
            SetItem(null);
        }
    }
}