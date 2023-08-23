using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void LaunchProjectile(float speed) => Rigidbody.AddForce(transform.up * speed, ForceMode2D.Impulse);
        public void EnableCollider() => _collider.enabled = true;
        public void DisableCollider() =>  _collider.enabled = false;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
                damagable.Damage(Damage, new [] {ItemsTags.All});
            
            Destroy(gameObject);
        }
    }
}
