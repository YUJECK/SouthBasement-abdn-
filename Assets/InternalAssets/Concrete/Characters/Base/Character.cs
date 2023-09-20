using SouthBasement.Characters.Components;
using SouthBasement.Characters.Base;

namespace SouthBasement.Characters
{
    public abstract class Character
    {
        public CharacterStats Stats { get; protected set; }
        public ComponentContainer Components { get; protected set; } = new();
        public CharacterGameObject GameObject { get; private set; }

        public virtual void OnCharacterPrefabSpawned(CharacterGameObject gameObject) 
            => GameObject = gameObject;
    }
}