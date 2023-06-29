using NTC.GlobalStateMachine;
using SouthBasement.Characters;

namespace SouthBasement.Generation
{
    public sealed class FightRoom : Room
    {
        private EnemiesHandler _enemiesFactory;

        protected override void OnAwake()
        {
            _enemiesFactory = GetComponentInChildren<EnemiesHandler>();
            _enemiesFactory.OnEnemiesDefeated += OnEnemiesDefeated;
        }

        private void OnEnemiesDefeated()
        {
            PassageHandler.OpenAllDoors();
            GlobalStateMachine.Push<IdleState>();
        }

        protected override void OnPlayerEntered(Character player)
        {
            _enemiesFactory.EnableEnemies();
            PassageHandler.CloseAllDoors();
            
            GlobalStateMachine.Push<FightState>();
        }
    }
}