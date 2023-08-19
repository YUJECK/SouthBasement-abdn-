namespace SouthBasement.InventorySystem
{
    public interface IAttackOverridable
    {
        bool UseCulldown();
        IDamagable[] Attack();
    }
}