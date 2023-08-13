namespace NTC.GlobalStateMachine
{
    public sealed class PausedState : GameState
    {
        public override GameStates State()
        {
            return GameStates.Paused;
        }
    }
}