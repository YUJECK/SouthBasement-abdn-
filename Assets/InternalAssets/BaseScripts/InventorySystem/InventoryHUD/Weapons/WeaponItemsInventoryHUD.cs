using SouthBasement.HUD;
using Zenject;

namespace SouthBasement.InventorySystem.Weapons
{
    public class WeaponItemsInventoryHUD : SlotHUD<WeaponSlot, WeaponItem>
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory) 
            => _inventory = inventory;

        private void Awake()
            => _slots = GetComponentsInChildren<WeaponSlot>();

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