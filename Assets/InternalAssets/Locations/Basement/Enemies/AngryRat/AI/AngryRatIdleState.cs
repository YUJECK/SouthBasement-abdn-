using NTC.ContextStateMachine;

namespace SouthBasement.AI
{
    public class AngryRatIdleState : State<AngryRatStateMachine>
    {
        public AngryRatIdleState(AngryRatStateMachine stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            Initializer.EnemyAnimator.PlayIdle();
        }
    }
}