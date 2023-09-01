namespace SouthBasement.InventorySystem
{
    public interface IAttackOverridable
    {
        bool UseCulldown();
        AttackResult Attack();
    }
}