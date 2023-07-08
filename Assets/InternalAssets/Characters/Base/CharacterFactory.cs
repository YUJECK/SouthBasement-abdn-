using SouthBasement.Characters;
using SouthBasement.Characters.Rat;
using SouthBasement.Helpers;
using UnityEngine;
using Zenject;

namespace TheRat.InternalAssets.Characters.Base
{
    public sealed class CharacterFactory : IInitializable
    {
        public Character Instance { get; private set; }
        
        private readonly DiContainer _diContainer;
        private readonly Transform _startPoint;

        public CharacterFactory(DiContainer diContainer, Transform startPoint)
        {
            _diContainer = diContainer;
            _startPoint = startPoint;
        }

        public void Initialize()
        {
            var characterPrefab = Resources.Load<Character>(ResourcesPathHelper.RatPrefab);

            var character = _diContainer
                .InstantiatePrefab(characterPrefab, _startPoint.position, Quaternion.identity, _startPoint)
                .GetComponent<Character>();

            _diContainer
                .BindInterfacesTo<RatCharacter>()
                .FromInstance(character)
                .AsSingle();
        }
    }
}