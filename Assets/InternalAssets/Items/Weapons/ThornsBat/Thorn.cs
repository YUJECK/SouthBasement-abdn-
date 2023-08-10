using System;
using UnityEngine;

namespace SouthBasement
{
    public sealed class Thorn : MonoBehaviour
    {
        private int _damage = 1;
        
        public void SetDamage(int damage) => _damage = damage;


        private void OnCollisionEnter2D(Collision2D other)
        {
            
            Debug.Log("sdkl;fj;dsfgsgasg;alks");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("sdkl;fj;lsdkfj;alks");
            
            if (other.TryGetComponent(out IDamagable damagable))
                damagable.Damage(_damage, new []{ "spawnedObject" });
        }
    }
}