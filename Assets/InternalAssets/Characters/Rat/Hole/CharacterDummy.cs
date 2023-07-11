using SouthBasement.Characters.Components;
using UnityEngine;

namespace SouthBasement.Characters.Hole
{
    public abstract class CharacterDummy : MonoBehaviour
    {
        public ComponentContainer Components { get; private set; } = new();

        public abstract CharacterConfig GetConfig();
    }
}