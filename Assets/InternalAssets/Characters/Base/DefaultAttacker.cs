using System;
using Cysharp.Threading.Tasks;
using SouthBasement.Helpers.Rotator;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters
{
    public sealed class DefaultAttacker : MonoBehaviour
    {
        [SerializeField] private ObjectRotator _attackPoint;

        public void Attack(int damage, float culldown, float range)
        {
            _attackPoint.Stop(culldown - 0.05f);

            var mask = LayerMask.GetMask("Enemy"); 
            
            var hits = Physics2D.OverlapCircleAll(_attackPoint.Point.transform.position, range, mask);

            foreach (var hit in hits)
            {
                if(!hit.isTrigger && hit.TryGetComponent<IDamagable>(out var damagable))
                    damagable.Damage(damage);
            }
        }
    }
}