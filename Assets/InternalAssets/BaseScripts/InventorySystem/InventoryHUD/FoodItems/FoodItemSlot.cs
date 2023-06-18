using SouthBasement.InventorySystem;
using UnityEngine.UI;

namespace SouthBasement.HUD.FoodItems
{
    public sealed class FoodItemSlot : InventorySlot<FoodItem>
    {
        private void Awake()
        {
            DefaultAwake();
            GetComponentInParent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            CurrentItem.Eat();
        }
    }
}