using System.Collections;
using System.Collections.Generic;
using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement
{
    public sealed class RatPyroMovement : State<RatPyroStateMachine>
    {
        private readonly List<Transform> _points;

        public RatPyroMovement(RatPyroStateMachine stateInitializer, List<Transform> points) : base(stateInitializer)
        {
            _points = points;
        }

        protected override void OnEnter()
        {
            Initializer.StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            while (IsActive)
            {
                MoveTarget = GetMoveTarget();

                Initializer.Agent.destination = MoveTarget.position;
                yield return new WaitUntil(MovingFinished);
            }
        }
        

        public Transform MoveTarget { get; set; }

        private Transform GetMoveTarget()
        {
            return _points[Random.Range(0, _points.Count)];
        }

        public bool MovingFinished() => Initializer.transform.position != MoveTarget.position;
    }
}