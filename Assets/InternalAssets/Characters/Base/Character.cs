using SouthBasement.Characters.Components;
using UnityEngine;

namespace SouthBasement.Characters
{
    public abstract class Character : MonoBehaviour 
    {
        [field: SerializeField] public CharacterConfig CharacterConfig { get; private set; }
        public CharacterStats Stats { get; protected set; }
        public ComponentContainer ComponentContainer { get; private set; } = new();
    }
}