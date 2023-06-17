using TheRat.HUD;
using UnityEngine;
using Zenject;

namespace TheRat.InventorySystem
{
    [AddComponentMenu("HUD/Inventory/ActiveItemsInventoryHUD")]
    public sealed class ActiveItemsInventoryHUD : SlotHUD<ActiveItemSlot, ActiveItem>
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
            
            inventory.OnAddedActiveItem.OnAdded += OnAdded;
            inventory.OnAddedActiveItem.OnRemoved += OnRemoved;
        }

        private void Awake() => SetSlotsInChildren();

        private void OnDestroy()
        {
            _inventory.OnAddedActiveItem.OnAdded -= OnAdded;
            _inventory.OnAddedActiveItem.OnRemoved -= OnRemoved;
        }
    }
}