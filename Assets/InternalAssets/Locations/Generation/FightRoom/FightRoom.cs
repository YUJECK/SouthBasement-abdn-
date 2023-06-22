using NTC.GlobalStateMachine;
using SouthBasement.Characters;

namespace SouthBasement.Generation
{
    public sealed class FightRoom : Room
    {
        private RoomEnemiesHandler _enemiesHandler;

        protected override void OnAwake()
        {
            _enemiesHandler = GetComponentInChildren<RoomEnemiesHandler>();
            _enemiesHandler.OnEnemiesDefeated += OnEnemiesDefeated;
        }

        private void OnEnemiesDefeated()
        {
            PassageHandler.OpenAllDoors();
            GlobalStateMachine.Push<IdleState>();
        }

        protected override void OnPlayerEntered(Character player)
        {
            _enemiesHandler.EnableEnemies();
            PassageHandler.CloseAllDoors();
            
            GlobalStateMachine.Push<FightState>();
        }
    }
}