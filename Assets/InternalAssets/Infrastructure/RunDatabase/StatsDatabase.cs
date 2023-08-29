using SouthBasement.Characters;
using SouthBasement.Helpers;
using SouthBasement.Infrastucture;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class StatsDatabase : IRunDatabase
    {
        private readonly DiContainer _diContainer;
        private ICoroutineRunner _coroutineRunner;

        public CharacterStats Stats { get; private set; }

        public bool Created { get; private set; }

        public StatsDatabase(DiContainer diContainer)
            => _diContainer = diContainer;

        public void Create()
        {
            if (Created)
            {
                Reset();                
                return;
            }
            
            _coroutineRunner = _diContainer.Resolve<ICoroutineRunner>();
            
            var configPrefab = Resources.Load<CharacterConfig>(ResourcesPathHelper.RatConfig);
            var config = ScriptableObject.Instantiate(configPrefab);
            
            Stats = new CharacterStats(config.DefaultStats);

            _diContainer
                .Bind<CharacterStats>()
                .FromInstance(Stats)
                .AsSingle();

            _diContainer
                .Bind<StaminaController>()
                .FromInstance(new StaminaController(Stats, _coroutineRunner))
                .AsSingle();

            Created = true;
        }

        public void Remove()
        {
            
        }

        public void Reset()
        {
            if (!Created) Create();
            
            _coroutineRunner = _diContainer.Resolve<ICoroutineRunner>();
            
            var configPrefab = Resources.Load<CharacterConfig>(ResourcesPathHelper.RatConfig);
            var config = ScriptableObject.Instantiate(configPrefab);
            
            Stats.Reset();
        }
    }
}