using NTC.ContextStateMachine;
using UnityEngine;

namespace AutumnForest.AI
{
    public sealed class WalkState : State<DefaultRatStateMachine>
    {
        private int _currentPoint = 0;
        
        public WalkState(DefaultRatStateMachine stateInitializer) : base(stateInitializer) { }

        public override void OnRun()
        {
            var current = Initializer.transform.position;
            var newPos = Vector3.MoveTowards(current, GetDestination(), Time.deltaTime);
            
            Initializer.transform.position = newPos;

            if (newPos == GetDestination())
                _currentPoint++;
            if (_currentPoint >= Initializer.RandomPoints.Length)
                _currentPoint = 0;

        }

        private Vector3 GetDestination()
        {
            return Initializer.RandomPoints[_currentPoint].position;
        }
    }
}