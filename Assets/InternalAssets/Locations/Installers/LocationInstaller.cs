using SouthBasement.Characters;
using SouthBasement.Characters.Rat;
using SouthBasement.Generation;
using SouthBasement.Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace SouthBasement.Locations
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;
        [FormerlySerializedAs("roomsContainer")] [SerializeField] private LevelConfig levelConfig;
        [SerializeField] private ContainersHelper containersHelper;

        public override void InstallBindings()
        {
            BindCharacter();
            BindRoomContainer();
            BindGeneration();

            Container
                .Bind<ContainersHelper>()
                .FromInstance(containersHelper)
                .AsSingle();
        }
        
        private void BindRoomContainer()
        {
            Container
                .Bind<LevelConfig>()
                .FromInstance(levelConfig)
                .AsSingle();
        }

        private void BindCharacter()
        {
            var characterPrefab = Resources.Load<Character>(ResourcesPathHelper.RatPrefab);

            var character = Container
                .InstantiatePrefab(characterPrefab, startPoint.position, Quaternion.identity, startPoint)
                .GetComponent<Character>();

            Container
                .Bind<Character>()
                .FromInstance(character)
                .AsSingle();
            
            Container
                .BindInterfacesTo<RatCharacter>()
                .FromInstance(character)
                .AsSingle();
        }

        private void BindGeneration()
        {
            Container
                .BindInterfacesAndSelfTo<GenerationController>()
                .FromInstance(new GenerationController(levelConfig, Container, startPoint))
                .AsSingle();
        }
    }
}