using System.Collections;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(FocusedFireTargetChecker))]
    public sealed class FocusedFireMovement : MonoBehaviour
    {
        [SerializeField] private AudioSource flySound;
        [SerializeField] private float moveSpeed;
        
        private Transform _startingPoint;
        private Transform _target;

        public void Awake()
            => GetComponent<FocusedFireTargetChecker>().OnTargeted += Move;

        private void ReturnToStartingPoint()
            => Move(_startingPoint);

        public void SetSpeed(float speed)
        {
            if(speed > 0)
                moveSpeed = speed;    
        }

        public FocusedFireMovement SetStartingPoint(Transform startingPoint)
        {
            if (startingPoint == null)
                
                Debug.LogError("Starting Point null");
            
            _startingPoint = startingPoint;
            return this;
        }
        
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