using SouthBasement.Characters;
using SouthBasement.Generation;
using SouthBasement.Helpers;
using SouthBasement.Characters.Rat;
using TheRat.InternalAssets.Characters.Base;
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
            Container
                .BindInterfacesTo<CharacterFactory>()
                .FromInstance(new CharacterFactory(Container, startPoint))
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