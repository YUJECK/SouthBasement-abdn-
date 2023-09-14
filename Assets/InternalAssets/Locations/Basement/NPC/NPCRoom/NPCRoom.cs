using NTC.GlobalStateMachine;
using SouthBasement.Characters.Base;

namespace SouthBasement.Generation.NPCRoom
{
    public class NPCRoom : Room
    {
        protected override void OnPlayerEntered(CharacterGameObject player)
            => GlobalStateMachine.Push<NPCState>();

        protected override void OnPlayerExit(CharacterGameObject player)
            => GlobalStateMachine.Push<IdleState>();
    }
}