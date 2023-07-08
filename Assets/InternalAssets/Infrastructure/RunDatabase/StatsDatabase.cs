using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using SouthBasement.Helpers;
using SouthBasement.Infrastucture;
using SouthBasement.InventorySystem;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    public sealed class StatsDatabase : IRunDatabase
    {
        private DiContainer _diContainer;
        private ICoroutineRunner _coroutineRunner;

        public CharacterStats Stats { get; private set; }

        public bool Created { get; private set; }

        public StatsDatabase(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Create()
        {
            if (Created) 
                return;
            
            _coroutineRunner = _diContainer.Resolve<ICoroutineRunner>();
            
            var configPrefab = Resources.Load<CharacterConfig>(ResourcesPathHelper.RatConfig);
            var config = ScriptableObject.Instantiate(configPrefab);
            
            Stats = new(config.DefaultStats);

            _diContainer
                .Bind<CharacterStats>()
                .FromInstance(Stats);

            _diContainer
                .Bind<CharacterAttackStats>()
                .FromInstance(Stats.AttackStats);

            _diContainer
                .Bind<CharacterHealthStats>()
                .FromInstance(Stats.HealthStats);

            _diContainer
                .Bind<CharacterMoveStats>()
                .FromInstance(Stats.MoveStats);

            _diContainer
                .Bind<CharacterStaminaStats>()
                .FromInstance(Stats.StaminaStats);

            _diContainer
                .Bind<StaminaController>()
                .FromInstance(new StaminaController(Stats.StaminaStats, _coroutineRunner));

            Created = true;
        }

        public void Reset()
        {
            if (!Created) Create();
            
            _coroutineRunner = _diContainer.Resolve<ICoroutineRunner>();
            
            var configPrefab = Resources.Load<CharacterConfig>(ResourcesPathHelper.RatConfig);
            var config = ScriptableObject.Instantiate(configPrefab);
            
            Stats = new(config.DefaultStats);

            _diContainer
                .Rebind<CharacterStats>()
                .FromInstance(Stats);

            _diContainer
                .Rebind<CharacterAttackStats>()
                .FromInstance(Stats.AttackStats);

            _diContainer
                .Rebind<CharacterHealthStats>()
                .FromInstance(Stats.HealthStats);

            _diContainer
                .Rebind<CharacterMoveStats>()
                .FromInstance(Stats.MoveStats);

            _diContainer
                .Rebind<CharacterStaminaStats>()
                .FromInstance(Stats.StaminaStats);

            _diContainer
                .Rebind<StaminaController>()
                .FromInstance(new StaminaController(Stats.StaminaStats, _coroutineRunner));
        }
    }
}