namespace NTC.GlobalStateMachine
{
    public class DiedState : GameState
    {
        public override bool CanRepeat => false;
        public override GameStates State()
        {
            return GameStates.Died;
        }
    }
}