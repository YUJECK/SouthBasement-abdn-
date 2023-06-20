using System;
using UnityEngine;

namespace TheRat.Characters.Stats
{
    [Serializable]
    public sealed class CharacterMoveStats
    {
        public Vector3 CurrentPosition { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField] public int DashStaminaRequire { get; set; } = 10;
    }
}