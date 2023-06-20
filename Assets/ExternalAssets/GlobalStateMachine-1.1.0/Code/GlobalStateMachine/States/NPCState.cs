namespace NTC.GlobalStateMachine
{
    public sealed class NPCState : GameState
    {
        public override bool CanRepeat => false;

        protected override void BlockNextStates()
        {
        }
    }
}