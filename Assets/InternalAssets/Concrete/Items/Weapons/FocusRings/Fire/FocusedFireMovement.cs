using System.Collections;
using SouthBasement.Characters;
using UnityEngine;
using Zenject;

namespace SouthBasement
{
    [RequireComponent(typeof(FocusedFireTargetChecker))]
    public sealed class FocusedFireMovement : MonoBehaviour
    {
        [SerializeField] private AudioSource flySound;
        [SerializeField] private float moveSpeed;
        
        private StaminaController _staminaController;
        
        private Transform _startingPoint;
        private Transform _target;
        
        public void Awake()
            => GetComponent<FocusedFireTargetChecker>().OnTargeted += AttackTarget;

        public FocusedFireMovement SetSpeed(float speed)
        {
            if(speed > 0)
                moveSpeed = speed;
            
            return this;
        }

        public FocusedFireMovement SetStaminaController(StaminaController staminaController)
        {
            _staminaController = staminaController;
            return this;
        }

        public FocusedFireMovement SetStartingPoint(Transform startingPoint)
        {
            if (startingPoint == null)
                Debug.LogError("Starting Point null");
            
            _startingPoint = startingPoint;
            return this;
        }

        private void AttackTarget(Transform target)
        {
            if (_staminaController.TryDo(10))
            {
                Move(target);
            }
        }

        private void ReturnToStartingPoint()
            => Move(_startingPoint);

        private void Move(Transform target)
        {
            _target = target;

            StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            flySound.Play();
            
            while (enabled && _target != null && transform.position != _target.position)
            {
                transform.position 
                    = Vector2.MoveTowards(transform.position, _target.position, moveSpeed * Time.deltaTime);
                
                yield return new WaitForFixedUpdate();
            }
            
            if(TargetWasLost())
                ReturnToStartingPoint();
            
            flySound.Stop();
        }

        private bool TargetWasLost() 
            => _target == null;
    }
}