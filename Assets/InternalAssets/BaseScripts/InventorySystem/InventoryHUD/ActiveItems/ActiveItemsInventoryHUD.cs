using SouthBasement.HUD;
using UnityEngine;
using Zenject;

namespace SouthBasement.InventorySystem
{
    [AddComponentMenu("HUD/Inventory/ActiveItemsInventoryHUD")]
    public sealed class ActiveItemsInventoryHUD : SlotHUD<ActiveItemSlot, ActiveItem>
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
            => _inventory = inventory;

        private void Awake()
        {
            SetSlotsInChildren();

            _inventory.OnAdded += OnAdded;
            _inventory.OnRemoved += OnRemoved;
        }

        private void OnDestroy()
        {
            _inventory.OnAdded -= OnAdded;
            _inventory.OnRemoved -= OnRemoved;
        }
    }
}