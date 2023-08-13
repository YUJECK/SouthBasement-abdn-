using UnityEngine;

namespace NTC.GlobalStateMachine
{
    public sealed class NPCState : GameState
    {
        public override GameStates State()
        {
            return GameStates.NPC;
        }
    }
}