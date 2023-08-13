namespace NTC.GlobalStateMachine
{
    public sealed class IdleState : GameState
    {
        public override GameStates State()
        {
            return GameStates.Idle;
        }
    }
}