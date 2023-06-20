using SouthBasement.InventorySystem;
using Zenject;

namespace SouthBasement.HUD.FoodItems
{
    public sealed class FoodSlotsHUD : SlotHUD<FoodItemSlot, FoodItem>
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
            => _inventory = inventory;

        private void Awake()
        {
            SetSlotsInChildren();
            UpdateInventory(_inventory);
        }

        private void OnEnable()
        {
            _inventory.OnAdded += OnAdded;
            _inventory.OnRemoved += OnRemoved;
        }

        private void OnDisable()
        {
            _inventory.OnAdded -= OnAdded;
            _inventory.OnRemoved -= OnRemoved;
        }
    }
}