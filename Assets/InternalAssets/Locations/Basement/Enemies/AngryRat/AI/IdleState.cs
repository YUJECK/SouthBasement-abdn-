using NTC.ContextStateMachine;

namespace SouthBasement.AI
{
    public class IdleState : State<AngryRatStateMachine>
    {
        public IdleState(AngryRatStateMachine stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            Initializer.EnemyAnimator.PlayIdle();
        }
    }
}