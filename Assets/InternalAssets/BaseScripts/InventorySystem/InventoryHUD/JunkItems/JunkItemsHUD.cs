using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement.HUD
{
    [AddComponentMenu("HUD/Inventory/JunkItemsHUD")]
    public class JunkItemsHUD : SlotHUD<JunkItemSlot, JunkItem>
    {
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory) => _inventory = inventory;

        private void Awake()
        {
            SetSlotsInChildren();
        }

        protected override void OnAdded(Item item)
        {
            if (item is JunkItem itemToAdd)
            {
                var slot = Find(itemToAdd.ItemID);

                if (slot != null)
                {
                    slot.AddItem();
                    return;
                }

                slot = GetEmpty();
                slot.SetItem(itemToAdd);
            }
        }

        protected override void OnRemoved(string itemID)
        {
            Find(itemID)?.RemoveItem();
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