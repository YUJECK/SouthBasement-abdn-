using SouthBasement.InventorySystem;
using UnityEngine.UI;

namespace SouthBasement.HUD
{
    public class JunkItemSlot : InventorySlot<JunkItem>
    {
        private void Awake()
        {
            ItemImage = GetComponent<Image>();
            SetItem(null);
        }
    }
}