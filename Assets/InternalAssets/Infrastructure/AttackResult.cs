using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement
{
    public sealed class AttackResult
    {
        public List<Collider2D> ColliderHits { get; private set; } = new();
        public List<IDamagable> DamagedHits { get; private set; } = new();

        public AttackResult(Collider2D[] colliderHits)
        {
            ProcessColliders(colliderHits);
        }

        private void ProcessColliders(Collider2D[] colliders)
        {
            foreach (var collider in colliders)
            {
                if (IsColliderFit(collider) && collider.TryGetComponent(out IDamagable damagable))
                {
                    ColliderHits.Add(collider);
                    DamagedHits.Add(damagable);
                }
            }
        }

        private static bool IsColliderFit(Collider2D collider)
            => collider != null && !collider.isTrigger;
    }
}