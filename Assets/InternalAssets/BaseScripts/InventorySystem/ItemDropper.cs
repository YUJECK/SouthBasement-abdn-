using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement.InventorySystem
{
    public sealed class ItemDropper
    {
        private Inventory _inventory;
        private ItemsContainer _itemsContainer;

        public ItemDropper(Inventory inventory, ItemsContainer itemsContainer)
        {
            _inventory = inventory;
            _itemsContainer = itemsContainer;
        }

        public void DropItem(Item itemToDrop, Vector3 spawnPosition, Vector2 direction, float speed = 15)
        {
            _itemsContainer
                .SpawnItem(itemToDrop, spawnPosition)
                .PlayMove(direction, speed);
            _inventory.RemoveItem(itemToDrop.ItemID);
        }
        public void DropItem(Item itemToDrop, Vector3 spawnPosition)
        {
            DropItem(itemToDrop, spawnPosition, Vector2.down);
        }
    }
}