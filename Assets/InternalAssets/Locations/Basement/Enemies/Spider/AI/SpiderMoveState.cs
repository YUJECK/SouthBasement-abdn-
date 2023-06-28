using System.Collections;
using NTC.ContextStateMachine;
using UnityEngine;

namespace SouthBasement
{
    public class SpiderMoveState : State<SpiderAI>
    {
        private const float MoveRate = 5f;

        private Transform _currentPoint;
        private Coroutine _moveCoroutine;
        
        public SpiderMoveState(SpiderAI stateInitializer) : base(stateInitializer) { }

        public override void OnEnter() => _moveCoroutine = Initializer.StartCoroutine(Move());
        public override void OnExit() => Initializer.StopCoroutine(_moveCoroutine);

        private IEnumerator Move()
        {
            while (Initializer.Enabled)
            {
                { //Поднимается
                    Initializer.CurrentlyHiding = true;
                    Initializer.Components.SpiderAnimator.PlayGoUp();
                }
                yield return new WaitForSeconds(0.45f);
                {//Опускается
                    _currentPoint = GetNewPoint();
                    Initializer.Components.SpiderMovement.Move(_currentPoint.position);
                    Initializer.Components.SpiderAnimator.PlayGoDown();
                }
                yield return new WaitForSeconds(0.45f);
                { // Висит, отдыхает
                    Initializer.Components.SpiderAnimator.PlayIdle();
                    Initializer.CurrentlyHiding = false;
                }
                yield return new WaitForSeconds(MoveRate);
            }
        }

        private Transform GetNewPoint()
        {
            var newPoint = _currentPoint;
            
            while (_currentPoint == newPoint)
                newPoint = Initializer.MovePoints[Random.Range(0, Initializer.MovePoints.Length)];

            return newPoint;
        }
    }
}
