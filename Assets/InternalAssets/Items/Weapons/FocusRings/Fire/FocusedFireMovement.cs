using System.Collections;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(FocusedFireTargetChecker))]
    public sealed class FocusedFireMovement : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        private Transform _target;

        private void Awake()
            => GetComponent<FocusedFireTargetChecker>().OnTargeted += Move;

        private void Move(Transform target)
        {
            _target = target;

            StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            while (enabled && _target != null)
            {
                transform.position 
                    = Vector2.MoveTowards(transform.position, _target.position, moveSpeed * Time.deltaTime);
                
                yield return new WaitForFixedUpdate();
            }
        }
    }
}