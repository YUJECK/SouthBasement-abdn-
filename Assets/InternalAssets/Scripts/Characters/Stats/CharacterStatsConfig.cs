namespace TheRat
{
    public sealed class CharacterStatsConfig
    {
        public int DefaultDamage { get; private set; } = 35;
        public float DefaultAttackRange { get; private set; } = 0.4f;
        public float DefaultAttackRate { get; private set; } = 1f;
        public float DefaultMoveSpeed { get; private set; } = 5f;
        public int DefaultStamina { get; private set; } = 100;
        
        public int DefaultMaximumHealth { get; private set; } = 60;
        public int DefaultCurrentHealth { get; private set; } = 60;
    }
}