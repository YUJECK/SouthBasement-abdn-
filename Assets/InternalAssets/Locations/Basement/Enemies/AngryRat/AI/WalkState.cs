using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement.AI
{
    public sealed class WalkState : State<AngryRatStateMachine>
    {
        private int _currentPoint = 0;
        
        public WalkState(AngryRatStateMachine stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            Initializer.EnemyAnimator.PlayWalk();
            Initializer.Movement.Blocked = false;
            Debug.Log("Run"); 
        }

        protected override void OnRun()
        {
            Initializer.Movement.Move(GetDestination());
        }

        protected override void OnExit()
        {
            Initializer.Movement.Blocked = true;
            Initializer.EnemyAnimator.PlayIdle();
        }

        private Vector3 GetDestination() => Initializer.TargetSelector.Target.transform.position;
    }
}