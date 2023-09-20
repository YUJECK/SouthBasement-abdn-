using NTC.ContextStateMachine;

namespace SouthBasement
{
    public sealed class SpiderAFKState : State<SpiderAI>
    {
        public SpiderAFKState(SpiderAI stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            Initializer.Components.Animator.PlayGoUp();
        }

        protected override void OnExit()
        {
            Initializer.Components.Animator.PlayGoDown();
        }
    }
}