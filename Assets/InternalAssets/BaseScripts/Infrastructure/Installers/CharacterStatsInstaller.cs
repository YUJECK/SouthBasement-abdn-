using SouthBasement.Characters;
using SouthBasement.Infrastucture;
using TheRat.Characters.Stats;
using Zenject;

namespace SouthBasement
{
    public sealed class CharacterStatsInstaller : MonoInstaller
    {
        private ICoroutineRunner _coroutineRunner;

        [Inject]
        private void Construct(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public override void InstallBindings()
        {
            CharacterStats stats = new();

            Container
                .Bind<CharacterStats>()
                .FromInstance(stats)
                .AsSingle();

            Container
                .Bind<CharacterAttackStats>()
                .FromInstance(stats.AttackStats)
                .AsSingle();
            
            Container
                .Bind<CharacterHealthStats>()
                .FromInstance(stats.HealthStats)
                .AsSingle();
            
            Container
                .Bind<CharacterMoveStats>()
                .FromInstance(stats.MoveStats)
                .AsSingle();
            
            Container
                .Bind<CharacterStaminaStats>()
                .FromInstance(stats.StaminaStats)
                .AsSingle();
            
            Container
                .Bind<StaminaController>()
                .FromInstance(new StaminaController(stats.StaminaStats, _coroutineRunner))
                .AsSingle();
        }
    }
}