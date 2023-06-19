using System;
using SouthBasement.Characters;
using SouthBasement.InventorySystem;
using SouthBasement.Items;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SouthBasement.HUD
{
    [RequireComponent(typeof(Image))]
    public sealed class PassiveItemsSlot : InventorySlot<PassiveItem>
    {
        private ItemsContainer _itemsContainer;
        private Character _character;
        private Inventory _inventory;

        [Inject]
        private void Construct(ItemsContainer itemsContainer, Character character, Inventory inventory)
        {
            _itemsContainer = itemsContainer;
            _character = character;
            _inventory = inventory;
        }
        
        private void Awake()
        {
            DefaultAwake();
            
            GetComponent<Button>().onClick.AddListener(DropItem);
        }

        private void DropItem()
        {
            Vector3 spawnPosition = _character.transform.position;
            
            _itemsContainer
                .SpawnItem(CurrentItem, spawnPosition)
                .PlayMove(new Vector3(0, -1f, 0f), 15);
            _inventory.RemoveItem(CurrentItem.ItemID);
        }
    }
}