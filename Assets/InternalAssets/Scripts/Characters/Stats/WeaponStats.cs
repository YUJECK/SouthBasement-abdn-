using System;

namespace TheRat.Weapons
{
    [Serializable]
    public sealed class WeaponStats
    {
        public int Damage { get; private set; } = 12;
        public float AttackRange { get; private set; } = 0.4f;
        public float AttackRate { get; private set; } = 1f;
        public int StaminaRequire { get; private set; } = 10;
    }
}