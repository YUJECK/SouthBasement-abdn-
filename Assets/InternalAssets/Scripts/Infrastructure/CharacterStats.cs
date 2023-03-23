namespace TheRat
{
    public sealed class CharacterStats
    {
        public int Damage { get; private set; } = 10;
        public float MoveSpeed { get; private set; } = 3.5f;
        public int MaximumHealth { get; private set; } = 60;
        public int CurrentHealth { get; private set; } = 60;

        //методы для их увеличения уменьшения
    }
}