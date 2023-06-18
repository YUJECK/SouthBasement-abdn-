using SouthBasement.Characters;
using SouthBasement.Generation;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;
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
            RoomsContainer roomsContainer = Resources.Load<RoomsContainer>(ResourcesPathHelper.RoomsContainer);

            Container
                .Bind<RoomsContainer>()
                .FromInstance(roomsContainer)
                .AsSingle();
        }

        private void BindCharacter()
        {
            Character characterPrefab = Resources.Load<Character>(ResourcesPathHelper.Player);

            Character character =
                Container
                    .InstantiatePrefab(characterPrefab, startPoint.position, startPoint.rotation, null)
                    .GetComponent<Character>();

            Container
                .Bind<Character>()
                .FromInstance(character)
                .AsSingle();
        }

        private void BindGeneration()
        {
            Container
                .BindInterfacesAndSelfTo<GenerationController>()
                .FromInstance(new GenerationController(Resources.Load<RoomsContainer>(ResourcesPathHelper.RoomsContainer),
                    Container, startPoint))
                .AsSingle();
        }
    }
}