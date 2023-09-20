using System;

namespace SouthBasement.InventorySystem.ItemBase
{
    public abstract class PassiveItem : Item
    {
        public override Type GetItemType()
            => typeof(PassiveItem);

        public virtual void OnRun() { }

        public override string GetStatsDescription()
            => ItemDescriptionEntryName;
    }
}