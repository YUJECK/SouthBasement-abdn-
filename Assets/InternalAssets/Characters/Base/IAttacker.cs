using SouthBasement.Weapons;

namespace SouthBasement.Characters
{
    public interface IAttacker
    {
        IDamagable[] Attack(CombatStats combatStats);
    }
}