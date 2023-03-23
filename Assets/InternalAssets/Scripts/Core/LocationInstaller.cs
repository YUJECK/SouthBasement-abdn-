using TheRat.Helpers;
using TheRat.LocationGeneration;
using TheRat.Player;
using UnityEngine;
using Zenject;

namespace TheRat
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private ContainersHelper _containersHelper;

        public override void InstallBindings()
        {
            BindCharacter();
            BindRoomStorager();

            Container
                .Bind<ContainersHelper>()
                .FromInstance(_containersHelper)
                .AsSingle();
        }

        private void BindRoomStorager()
        {
            RoomsStorager roomsStorager = Resources.Load<RoomsStorager>(AssetsPath.RoomsStorager);

            Container
                .Bind<RoomsStorager>()
                .FromInstance(roomsStorager)
                .AsSingle();
        }

        private void BindCharacter()
        {
            Character characterPrefab = Resources.Load<Character>(AssetsPath.Player);

            Character character = 
                Container
                .InstantiatePrefab(characterPrefab, _startPoint.position, _startPoint.rotation, null)
                .GetComponent<Character>();
            
            Container
                .Bind<Character>()
                .FromInstance(character)
                .AsSingle();
        }
    }
}