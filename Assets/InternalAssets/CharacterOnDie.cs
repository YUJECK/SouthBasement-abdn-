using Cinemachine;
using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class CharacterOnDie : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera diedCamera;
        private Character _character;

        [Inject]
        private void Construct(Character character, CharacterHealthStats characterHealthStats)
        {
            _character = character;
            characterHealthStats.OnDied += OnDied;
        }

        private void OnDied()
        {
            diedCamera.gameObject.SetActive(true);
            
            _character.Movable.CanMove = false;
            _character.Attackable.Blocked = true;
            _character.Dashable.Blocked = true;
            
        }
    }
}