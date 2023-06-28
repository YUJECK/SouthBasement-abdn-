using NTC.GlobalStateMachine;
using SouthBasement.Characters;

namespace SouthBasement.Generation.NPCRoom
{
    public sealed class NPCRoom : Room
    {
        protected override void OnPlayerEntered(Character player)
        {
            GlobalStateMachine.Push<NPCState>();
        }

        protected override void OnPlayerExit(Character player)
        {
            GlobalStateMachine.Push<IdleState>();
        }
    }
}