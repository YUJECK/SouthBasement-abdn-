using NTC.ContextStateMachine;

namespace SouthBasement.AI
{
    public class IdleState : State<DefaultRatStateMachine>
    {
        public IdleState(DefaultRatStateMachine stateInitializer) : base(stateInitializer) { }

        public override void OnEnter()
        {
            Initializer.EnemyAnimator.PlayIdle();
        }
    }
}