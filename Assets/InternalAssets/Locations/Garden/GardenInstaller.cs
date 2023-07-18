using SouthBasement.Characters.Base;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;

namespace SouthBasement.Garden
{
    public sealed class GardenInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;
        private RunController _runController;

        [Inject]
        private void Construct(RunController runController) 
            => _runController = runController;

        public override void InstallBindings()
        {
            var characterPrefab = Resources.Load<CharacterGameObject>(ResourcesPathHelper.RatPrefab);

            var character = Container
                .InstantiatePrefab(characterPrefab, startPoint.position, Quaternion.identity, startPoint)
                .GetComponent<CharacterGameObject>();

            _runController.OnCharacterSpawned(character);    
        }
    }
}