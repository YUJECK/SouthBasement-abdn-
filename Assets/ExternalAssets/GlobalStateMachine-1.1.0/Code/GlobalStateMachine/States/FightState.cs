namespace NTC.GlobalStateMachine
{
    public sealed class FightState : GameState
    {
        public override GameStates State()
        {
            return GameStates.Fight;
        }
    }
}