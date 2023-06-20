using SouthBasement.Weapons;

namespace TheRat.Characters.Stats
{
    public sealed class CharacterAttackStats
    {
        public AttackStatsConfig CurrentStats { get; set; } = new();
        public AttackStatsConfig DefaultAttackStatsConfig { get; private set; } = new();
    }
}