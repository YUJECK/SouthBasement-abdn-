using System;
using SouthBasement.InventorySystem;
using Zenject;

namespace SouthBasement.HUD
{
    public class JunkItemsHUD : SlotHUD<JunkItemSlot, JunkItem>
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory) => _inventory = inventory;

        private void Awake()
            => SetSlotsInChildren();

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