using SouthBasement.Weapons;

namespace SouthBasement.Characters
{
    public interface IAttacker
    {
        AttackResult Attack(CombatStats combatStats);
    }
}