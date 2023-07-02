using NTC.ContextStateMachine;

namespace SouthBasement.AI
{
    public sealed class AFKState : State<AngryRatStateMachine>
    {
        public AFKState(AngryRatStateMachine stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter() => Initializer.EnemyAnimator.PlayAFK();
    }
}