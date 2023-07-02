using System.Collections;
using NTC.ContextStateMachine;
using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement
{
    public sealed class SpiderFloorMoveState : State<SpiderAI>
    {
        private MovePoint _currentPoint;
        private Coroutine _movingCoroutine;
        
        public SpiderFloorMoveState(SpiderAI stateInitializer) : base(stateInitializer) { }

        protected override void OnEnter()
        {
            if(!Initializer.FallenDown)
                Initializer.Fall();

            _movingCoroutine = Initializer.StartCoroutine(Moving());
        }

        protected override void OnExit()
        {
            Initializer.StopCoroutine(_movingCoroutine);
        }

        private IEnumerator Moving()
        {
            while (Initializer.Enabled)
            {
                if (_currentPoint != null && _currentPoint.CurrentEnemy == Initializer)
                    _currentPoint.CurrentEnemy = null;
                
                Initializer.Components.AudioPlayer.PlayWalk();
                _currentPoint = GetNewPoint();
                _currentPoint.CurrentEnemy = Initializer;
                
                Initializer.Components.SpiderMovement.Walk(_currentPoint.transform.position);
                Initializer.Components.Animator.PlayWalk();
                
                while (Initializer.transform.position.x != _currentPoint.transform.position.x && Initializer.transform.position.y != _currentPoint.transform.position.y)
                    yield return null;                    
                
                Initializer.Components.AudioPlayer.StopWalk(); 
                Initializer.Components.Animator.PlayAfraid();
                yield return new WaitForSeconds(2f);
            }
        }
        
        private MovePoint GetNewPoint()
        {
            var newPoint = _currentPoint;

            while (_currentPoint == newPoint || newPoint.CurrentEnemy != null)
            {
                newPoint = Initializer.MovePoints[Random.Range(0, Initializer.MovePoints.Length)];
            }

            return newPoint;
        }
    }
}