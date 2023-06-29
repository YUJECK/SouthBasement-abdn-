using NTC.ContextStateMachine;

namespace SouthBasement
{
    public sealed class SpiderAFKState : State<SpiderAI>
    {
        public SpiderAFKState(SpiderAI stateInitializer) : base(stateInitializer) { }

        public override void OnEnter()
        {
            Initializer.Components.Animator.PlayGoUp();
        }

        public override void OnExit()
        {
            Initializer.Components.Animator.PlayGoDown();
        }
    }
}