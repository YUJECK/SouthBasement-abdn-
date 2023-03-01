using TheRat.Player;
using UnityEngine;
using Zenject;

namespace TheRat
{
    public sealed class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Character character;
        [SerializeField] private Transform startPoint;

        public override void InstallBindings()
        {
            InstallCharacter();
        }

        private void InstallCharacter()
        {
            RatController character = Container.InstantiatePrefab(this.character, startPoint.position, startPoint.rotation, null).GetComponent<RatController>();
            Container.Bind<Character>().FromInstance(character);
        }
    }
}