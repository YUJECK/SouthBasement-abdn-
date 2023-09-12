namespace SouthBasement.InventorySystem.ItemBase
{
    public interface IAttackOverridable
    {
        bool UseCulldown();
        AttackResult Attack();
    }
}