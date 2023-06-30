using System.Collections;
using NaughtyAttributes.Test;
using NTC.ContextStateMachine;
using SouthBasement.AI;
using UnityEngine;

namespace SouthBasement
{
    public class SpiderMoveState : State<SpiderAI>
    {
        private const float MoveRate = 5f;

        private MovePoint _currentPoint;
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
                    Initializer.Components.Animator.PlayGoUp();
                }
                yield return new WaitForSeconds(0.45f);
                {//Опускается
                    
                    if(_currentPoint != null)
                        _currentPoint.CurrentEnemy = null;
                    
                    _currentPoint = GetNewPoint();
                    _currentPoint.CurrentEnemy = Initializer;
                    
                    Initializer.Components.SpiderMovement.Move(_currentPoint.transform.position);
                    Initializer.Components.Animator.PlayGoDown();
                }
                yield return new WaitForSeconds(0.45f);
                { // Висит, отдыхает
                    Initializer.Components.Animator.PlayIdle();
                    Initializer.CurrentlyHiding = false;
                }
                yield return new WaitForSeconds(MoveRate + Random.Range(-0.5f, 0.5f));
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
