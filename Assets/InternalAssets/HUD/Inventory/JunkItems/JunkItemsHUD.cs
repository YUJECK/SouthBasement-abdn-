using System;
using TheRat.InventorySystem;
using Zenject;

namespace TheRat.HUD
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
            _inventory.OnAddedJunkItem.OnAdded += OnAdded;
            _inventory.OnAddedJunkItem.OnRemoved += OnRemoved;
        }

        private void OnDisable()
        {
            _inventory.OnAddedJunkItem.OnAdded -= OnAdded;
            _inventory.OnAddedJunkItem.OnRemoved -= OnRemoved;
        }
    }
}