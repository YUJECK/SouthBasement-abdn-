using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
                damagable.Damage(Damage, new [] { "" });
            
            Destroy(gameObject);
        }
    }
}
