using SouthBasement.InventorySystem;

namespace TheRat.InventoryHUD.Base
{
    public interface IInventorySlot
    {
        Item CurrentItemNonGeneric { get; }
    }
}