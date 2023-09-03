namespace SouthBasement.InternalAssets.InventorySystem.ItemBase
{
    public abstract class PassiveItem : Item
    {
        public virtual void OnRun() { }

        public override string GetStatsDescription()
            => ItemDescriptionEntryName;
    }
}