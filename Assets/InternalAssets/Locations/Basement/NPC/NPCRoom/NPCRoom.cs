using NTC.GlobalStateMachine;
using SouthBasement.Characters;
using SouthBasement.Characters.Base;

namespace SouthBasement.Generation.NPCRoom
{
    public sealed class NPCRoom : Room
    {
        protected override void OnPlayerEntered(CharacterGameObject player)
        {
            GlobalStateMachine.Push<NPCState>();
        }

        protected override void OnPlayerExit(CharacterGameObject player)
        {
            GlobalStateMachine.Push<IdleState>();
        }
    }
}