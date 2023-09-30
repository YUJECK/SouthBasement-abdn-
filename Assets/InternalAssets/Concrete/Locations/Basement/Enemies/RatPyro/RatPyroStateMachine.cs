using NTC.ContextStateMachine;
using SouthBasement.AI;
using UnityEngine;
using UnityEngine.AI;

namespace SouthBasement
{
    public sealed class RatPyroStateMachine : Enemy
    {
        private readonly StateMachine<RatPyroStateMachine> _stateMachine = new();
        public NavMeshAgent Agent { get; set; }

        private void Start()
        {
            _stateMachine.AddStates();
        }
    }
}