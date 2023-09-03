using System;

namespace SouthBasement.InternalAssets.InventorySystem.ItemBase
{
    public abstract class FoodItem : Item
    {
        public override Type GetItemType()
            => typeof(FoodItem);

        public abstract void Eat();
    }
}