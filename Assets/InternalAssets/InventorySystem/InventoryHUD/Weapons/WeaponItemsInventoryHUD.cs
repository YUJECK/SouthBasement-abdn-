using SouthBasement.HUD;
using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using UnityEngine;
using Zenject;

namespace SouthBasement.InventorySystem.Weapons
{
    [AddComponentMenu("HUD/Inventory/WeaponItemsInventoryHUD")]
    public class WeaponItemsInventoryHUD : SlotHUD<WeaponSlot, WeaponItem>
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory) 
            => _inventory = inventory;

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