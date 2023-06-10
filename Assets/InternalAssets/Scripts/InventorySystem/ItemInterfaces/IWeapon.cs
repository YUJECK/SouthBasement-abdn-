namespace TheRat.InventorySystem
{
    public interface IWeapon
    {
        int Damage { get; }
        
        void OnAttack();
    }
}