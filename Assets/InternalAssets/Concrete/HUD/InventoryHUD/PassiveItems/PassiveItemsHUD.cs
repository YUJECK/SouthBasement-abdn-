using SouthBasement.InventorySystem.ItemBase;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.HUD
{
    [AddComponentMenu("HUD/Inventory/PassiveItemsHUD")]
    public sealed class PassiveItemsHUD : SlotHUD<PassiveItemsSlot, PassiveItem>
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory) 
            => _inventory = inventory;

        private void Awake()
        {
            SetSlotsInChildren();
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