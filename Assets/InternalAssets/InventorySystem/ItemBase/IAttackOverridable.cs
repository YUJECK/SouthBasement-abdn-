namespace SouthBasement.InternalAssets.InventorySystem.ItemBase
{
    public interface IAttackOverridable
    {
        bool UseCulldown();
        AttackResult Attack();
    }
}