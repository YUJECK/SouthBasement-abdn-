using SouthBasement.Characters;
using SouthBasement.Characters.Stats;
using SouthBasement.Helpers;
using SouthBasement.Infrastucture;
using SouthBasement.InventorySystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace SouthBasement.Infrastructure
{
    public sealed class RunStarter
    {
        private DiContainer _diContainer;
        private ICoroutineRunner _coroutineRunner;

        [Inject]
        public RunStarter(DiContainer diContainer, ICoroutineRunner coroutineRunner)
        {
            _diContainer = diContainer;
            _coroutineRunner = coroutineRunner;
        }
        
        public void StartRun()
        {
            _coroutineRunner = _diContainer.Resolve<ICoroutineRunner>();
            
            CharacterConfig configPrefab = Resources.Load<CharacterConfig>(ResourcesPathHelper.RatConfig);
            var config = Object.Instantiate(configPrefab);
            CharacterStats stats = new(config.DefaultStats);

            _diContainer
                .Rebind<CharacterStats>()
                .FromInstance(stats);

            _diContainer
                .Rebind<CharacterAttackStats>()
                .FromInstance(stats.AttackStats);

            _diContainer
                .Rebind<CharacterHealthStats>()
                .FromInstance(stats.HealthStats);

            _diContainer
                .Rebind<CharacterMoveStats>()
                .FromInstance(stats.MoveStats);

            _diContainer
                .Rebind<CharacterStaminaStats>()
                .FromInstance(stats.StaminaStats);

            _diContainer
                .Rebind<StaminaController>()
                .FromInstance(new StaminaController(stats.StaminaStats, _coroutineRunner));

            _diContainer.Rebind<Inventory>().FromInstance(new Inventory(_diContainer));
            SceneManager.LoadScene("FirstLevel");
        }
    }
}