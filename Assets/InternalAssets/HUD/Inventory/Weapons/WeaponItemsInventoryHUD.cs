using TheRat.HUD;
using Zenject;

namespace TheRat.InventorySystem.Weapons
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
            _inventory.OnAddedWeapon.OnAdded += OnAdded;
            _inventory.OnAddedWeapon.OnRemoved += OnRemoved;
        }

        private void OnDisable()
        {
            _inventory.OnAddedWeapon.OnAdded -= OnAdded;
            _inventory.OnAddedWeapon.OnRemoved -= OnRemoved;
        }
    }
}