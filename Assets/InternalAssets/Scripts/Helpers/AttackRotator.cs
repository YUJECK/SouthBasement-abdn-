using System.Collections;
using UnityEngine;

namespace TheRat.InternalAssets.Scripts.Helpers
{
    public sealed class AttackRotator : MonoBehaviour
    {
        private bool _stopped;
        [field: SerializeField] public Transform Target { get; set; }

        [field: SerializeField] public Transform Point { get; private set; } 
        [field: SerializeField] public float Coefficient { get; set; } = 1f;

        private void FixedUpdate()
        {
            if(!_stopped)
                transform.rotation = Quaternion.Euler(0, 0, GetAngle());
        }
        
        private float GetAngle()
        {
            Vector2 direction = Target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            
            return Coefficient * angle;
        }

        public void Stop(float time) => StartCoroutine(StopRoutine(time));

        private IEnumerator StopRoutine(float time)
        {
            _stopped = true;
            yield return new WaitForSeconds(time);
            _stopped = false;
        }
    }
}