using TheRat.Helpers;
using TheRat.LocationGeneration;
using TheRat.Player;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

namespace TheRat
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;

        public override void InstallBindings()
        {
            InstallCharacter();

            RoomsStorager roomsStorager = Resources.Load<RoomsStorager>(AssetsPath.RoomsStorager);
            Container
                .Bind<RoomsStorager>()
                .FromInstance(roomsStorager)
                .AsSingle();
        }

        private void InstallCharacter()
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
    }
}