using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement
{
    public sealed class Thorn : MonoBehaviour
    {
        private int _damage = 4;
        
        public void SetDamage(int damage) => _damage = damage;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IDamagable damagable))
                damagable.Damage(_damage, new [] {AttackTags.ExtraObject});
        }
    }
}