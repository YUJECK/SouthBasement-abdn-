using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SouthBasement.InventorySystem
{
    public sealed class PassiveItemsUsage : MonoBehaviour
    {
        private Dictionary<string, PassiveItem> _passiveItems = new();
        private Inventory _inventory;

        [Inject]
        private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }

        private void OnRemoved(string itemID)
        {
            _passiveItems[itemID].OnPutOut();
            _passiveItems.Remove(itemID);
        }

        private void OnAddedItem(Item item)
        {
            if (item is PassiveItem passiveItem)
            {
                _passiveItems.Add(passiveItem.ItemID, passiveItem);
                passiveItem.OnPutOn();
            }
        }

        private void OnEnable()
        {
            _inventory.OnAdded += OnAddedItem;
            _inventory.OnRemoved += OnRemoved;
        }

        private void OnDisable()
        {
            _inventory.OnAdded -= OnAddedItem;
            _inventory.OnRemoved -= OnRemoved;
        }

        private void Update()
        {
            foreach(var passiveItem in _passiveItems)
                passiveItem.Value.OnRun();
        }
    }
}