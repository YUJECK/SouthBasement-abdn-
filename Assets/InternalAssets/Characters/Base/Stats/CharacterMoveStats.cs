using UnityEngine;

namespace TheRat.Characters.Stats
{
    public sealed class CharacterMoveStats
    {
        public Vector3 CurrentPosition { get; private set; }
        public float MoveSpeed { get; private set; } = 5f;
        public int DashStaminaRequire { get; set; } = 10;
    }
}