using UnityEngine;

namespace TheRat.InternalAssets.Scripts.Helpers
{
    public sealed class Rotator : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; set; }

        [field: SerializeField] public float Coefficient { get; set; } = 1f;

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.Euler(0, 0, GetAngle());            
        }
        
        private float GetAngle()
        {
            Vector2 direction = Target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            
            return Coefficient * angle;
        }
    }
}