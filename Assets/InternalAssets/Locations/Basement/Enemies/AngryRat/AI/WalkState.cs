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
            Initializer.Agent.isStopped = false;
        }

        public override void OnRun()
        {
            Initializer.Agent.SetDestination(GetDestination());
        }

        public override void OnExit()
        {
            Initializer.Agent.isStopped = true;
            Initializer.EnemyAnimator.PlayIdle();
        }

        private Vector3 GetDestination()
        {
            return Initializer.TargetSelector.Target.transform.position;
        }
    }
}