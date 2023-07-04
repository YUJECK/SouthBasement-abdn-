using NTC.ContextStateMachine;
using SouthBasement.AI;
using UnityEngine.AI;

namespace SouthBasement.Basement.Enemies.LittleRat
{
    public sealed class LittleRunAwayState : State<AngryRatStateMachine>
    {
        public LittleRunAwayState(LittleRatAI stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            Initializer.Movement.Blocked = false;
            Initializer.GetComponent<NavMeshAgent>().speed += 1.5f;

            var point = (Initializer as LittleRatAI)?.MovePointsHandler.GetFurthest(Initializer.transform);
            Initializer.Movement.Move(point.transform.position, OnPathCompleted);
        }

        private void OnPathCompleted()       
        {
            var littleRatAI = (Initializer as LittleRatAI);
            littleRatAI.CanRunAway = false;
            Initializer.GetComponent<NavMeshAgent>().speed -= 1.5f;
            Initializer.Movement.Blocked = true;
        }
    }
}