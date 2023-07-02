using NTC.ContextStateMachine;
using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement.Basement.Enemies.LittleRat
{
    public sealed class LittleRunAwayState : State<AngryRatStateMachine>
    {
        public LittleRunAwayState(LittleRatAI stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            var point = (Initializer as LittleRatAI).MovePoints[Random.Range(0, (Initializer as LittleRatAI).MovePoints.Length)];
            Initializer.Movement.Move(point.transform.position, test);              
        }

        private void test()       
        {
            var test2 = (Initializer as LittleRatAI);
                test2.CanRunAway = false;
        }
    }
}