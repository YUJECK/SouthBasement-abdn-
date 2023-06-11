namespace TheRat
{
    public sealed class CharacterStats
    {
        public int Damage { get; private set; } = 35;
        public float AttackRange { get; private set; } = 0.4f;
        public float AttackRate { get; private set; } = 1f;
        public float MoveSpeed { get; private set; } = 5f;
        public int MaximumHealth { get; private set; } = 60;
        public int CurrentHealth { get; private set; } = 60;

        //методы для их увеличения уменьшения
    }
}