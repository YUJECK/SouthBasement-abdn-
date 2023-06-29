using NTC.ContextStateMachine;

namespace SouthBasement
{
    public sealed class SpiderAFKState : State<SpiderAI>
    {
        public SpiderAFKState(SpiderAI stateInitializer) : base(stateInitializer) { }

        public override void OnEnter()
        {
            Initializer.Components.SpiderAnimator.PlayGoUp();
        }

        public override void OnExit()
        {
            Initializer.Components.SpiderAnimator.PlayGoDown();
        }
    }
}