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
            Initializer.Agent.destination = GetDestination(); 
            
            if (Initializer.transform.position == GetDestination())
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