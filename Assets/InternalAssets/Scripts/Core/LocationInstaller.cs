using TheRat.Generation;
using TheRat.Helpers;
using TheRat.Player;
using UnityEngine;
using Zenject;

namespace TheRat
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
            RoomsContainer roomsContainer = Resources.Load<RoomsContainer>(AssetsPath.RoomsContainer);
        
            Container
                .Bind<RoomsContainer>()
                .FromInstance(roomsContainer)
                .AsSingle();
        }

        private void BindCharacter()
        {
            Character characterPrefab = Resources.Load<Character>(AssetsPath.Player);

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
                .FromInstance(new GenerationController(Resources.Load<RoomsContainer>(AssetsPath.RoomsContainer), Container, startPoint))
                .AsSingle();
        }
    }
}