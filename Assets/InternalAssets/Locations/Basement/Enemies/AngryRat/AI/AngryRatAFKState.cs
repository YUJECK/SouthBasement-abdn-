using NTC.ContextStateMachine;

namespace SouthBasement.AI
{
    public sealed class AngryRatAFKState : State<AngryRatStateMachine>
    {
        public AngryRatAFKState(AngryRatStateMachine stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter() => Initializer.EnemyAnimator.PlayAFK();
    }
}