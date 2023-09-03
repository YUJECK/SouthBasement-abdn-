using SouthBasement.InternalAssets.InventorySystem.ItemBase;
using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public sealed class ItemDropper
    {
        private readonly Inventory _inventory;
        private readonly ItemsContainer _itemsContainer;

        public ItemDropper(Inventory inventory, ItemsContainer itemsContainer)
        {
            _inventory = inventory;
            _itemsContainer = itemsContainer;
        }

        public void DropItem(Item itemToDrop, Vector3 spawnPosition, Vector2 direction, float speed = 15)
        {
            if (itemToDrop == null)
            {
                Debug.LogWarning("You tried to drop item");
                return;
            }
            
            _itemsContainer
                .SpawnItem(itemToDrop, spawnPosition)
                .PlayMove(direction, speed);
            
            _inventory.RemoveItem(itemToDrop.ItemID);
        }
        
        public void DropItem(Item itemToDrop, Vector3 spawnPosition)
            => DropItem(itemToDrop, spawnPosition, Vector2.down);
    }
}