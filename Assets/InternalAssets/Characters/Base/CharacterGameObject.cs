using UnityEngine;
using Zenject;

namespace SouthBasement.Characters.Base
{
    public class CharacterGameObject : MonoBehaviour
    {
        public Character Character { get; protected set; }

        [Inject]
        private void Construct(Character character)
        {
            Character = character;
        }

        private void Update()
        {
            Character.Components.UpdateALl();
        }
    }
}