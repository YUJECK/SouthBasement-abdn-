using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement.Basement.Enemies.ArmouredRat.AI
{
    public sealed class ArmouredRatWalkState : State<ArmouredRatAI>
    {
        public ArmouredRatWalkState(ArmouredRatAI stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            Initializer.EnemyMovable.Blocked = false;
            Initializer.EnemyAnimator.PlayWalk();
        }

        protected override void OnRun() => Initializer.EnemyMovable.Move(GetTargetPosition());
        protected override void OnExit()
        {
            Initializer.EnemyMovable.Blocked = true;
            Initializer.EnemyAnimator.PlayIdle();
        }

        private Vector3 GetTargetPosition() => Initializer.TargetSelector.Target.transform.position;
    }
}