using SouthBasement;
using SouthBasement.Characters;
using SouthBasement.Extensions;
using UnityEngine;
using Zenject;

namespace TheRat
{
    public class PlayerWalkSoundOverrider : MonoBehaviour
    {
        public AudioSource walkSound;
        
        [Inject]
        private void Construct(Character character)
        {
            character.GameObject.gameObject.Get<CharacterAudioPlayer>().walkSound = walkSound;
        }
    }
}
