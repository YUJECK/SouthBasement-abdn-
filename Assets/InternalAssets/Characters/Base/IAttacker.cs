using SouthBasement.Items;

namespace SouthBasement.Characters
{
    public interface IAttacker
    {
        IDamagable[] Attack(int damage, float culldown, float range, ItemsTags[] args);
    }
}