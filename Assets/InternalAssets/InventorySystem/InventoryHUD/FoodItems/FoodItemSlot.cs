using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace SouthBasement.HUD.FoodItems
{
    [AddComponentMenu("HUD/Inventory/FoodItemSlot")]
    public sealed class FoodItemSlot : InventorySlot<FoodItem>
    {
        private void Start()
        {
            DefaultStart();
            GetComponentInParent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
            => CurrentItem?.Eat();
    }
}