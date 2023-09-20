using System.Collections;
using UnityEngine;

namespace SouthBasement.Helpers.Rotator
{
    public class ObjectRotator : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; set; }

        [field: SerializeField] public Transform Point { get; private set; } 
        [field: SerializeField] public float Coefficient { get; set; } = 1f;
        
        protected bool Stopped;

        protected virtual void FixedUpdate()
        {
            if(!Stopped)
                transform.rotation = Quaternion.Euler(0, 0, GetAngle());
        }
        
        protected virtual float GetAngle()
        {
            if (Target == null) return 0f;
            
            Vector2 direction = Target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            
            return Coefficient * angle;
        }

        public virtual void Stop(float time) => StartCoroutine(StopRoutine(time));

        protected virtual IEnumerator StopRoutine(float time)
        {
            Stopped = true;
            yield return new WaitForSeconds(time);
            Stopped = false;
        }
    }
}