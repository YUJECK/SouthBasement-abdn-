using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement.AI
{
    public sealed class AngryRatWalkState : State<AngryRatStateMachine>
    {
        private int _currentPoint = 0;
        
        public AngryRatWalkState(AngryRatStateMachine stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter() => Initializer.EnemyAnimator.PlayWalk();
        protected override void OnRun() => Initializer.Movement.Move(GetDestination());
        protected override void OnExit() => Initializer.EnemyAnimator.PlayIdle();

        private Vector3 GetDestination() => Initializer.TargetSelector.Target.transform.position;
    }
}