using System;
using SouthBasement.InventorySystem;

namespace SouthBasement.HUD
{
    public interface ISlotHUD
    {
        Type GetTypeHUD();
        void UpdateInventory(Inventory inventory);
    }
}