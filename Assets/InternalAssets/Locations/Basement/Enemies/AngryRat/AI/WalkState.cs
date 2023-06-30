using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement.AI
{
    public sealed class WalkState : State<DefaultRatStateMachine>
    {
        private int _currentPoint = 0;
        
        public WalkState(DefaultRatStateMachine stateInitializer) : base(stateInitializer) { }

        public override void OnEnter()
        {
            Initializer.EnemyAnimator.PlayWalk();
            Initializer.Movement.Blocked = false;
        }

        public override void OnRun()
        {
            Initializer.Movement.Move(GetDestination());
        }

        public override void OnExit()
        {
            Initializer.Movement.Blocked = true;
            Initializer.EnemyAnimator.PlayIdle();
        }

        private Vector3 GetDestination() => Initializer.TargetSelector.Target.transform.position;
    }
}