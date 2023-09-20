using System.Collections;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(CarrotDroneBomb))]
    [RequireComponent(typeof(CarrotDroneTargetController))]
    public sealed class CarrotDroneMovement : CarrotDroneComponent
    {
        private CarrotDroneBomb _bomb;
        private Coroutine _explodeCoroutine;
        private CarrotDroneTargetController _targetController;

        private Transform CurrentTarget => _targetController.CurrentTarget;

        private void Start()
        {
            _targetController = GetComponent<CarrotDroneTargetController>();
            _bomb = GetComponent<CarrotDroneBomb>();
        }

        private void Update()
        {
            if (CanStartExplodeTimer())
            {
                _explodeCoroutine = StartCoroutine(WaitForExplode());
                return;
            }

            if (CanMove())
            {
                Move();
                
                if(_explodeCoroutine != null)
                    StopCoroutine(_explodeCoroutine);
            }
        }

        private bool CanMove() => CurrentTarget != null;

        private bool CanStartExplodeTimer() => CurrentTarget == null && _explodeCoroutine == null;

        private void Move()
        {
            transform.position
                = Vector3.MoveTowards(transform.position, CurrentTarget.position,
                    Time.deltaTime * CarrotDroneConfig.moveSpeed);
        }

        public IEnumerator WaitForExplode()
        {
            yield return new WaitForSeconds(CarrotDroneConfig.timeToExplode);
            _bomb.Explode();
        }
    }
}