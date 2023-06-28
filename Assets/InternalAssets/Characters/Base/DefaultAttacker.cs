using System.Collections.Generic;
using SouthBasement.Helpers.Rotator;
using SouthBasement.InventorySystem;
using UnityEngine;

namespace SouthBasement.Characters
{
    public sealed class DefaultAttacker : MonoBehaviour
    {
        [SerializeField] private ObjectRotator _attackPoint;

        public IDamagable[] Attack(int damage, float culldown, float range, WeaponItem weaponItem)
        {
            List<IDamagable> hitted = new(); 
            _attackPoint.Stop(culldown - 0.05f);

            var mask = LayerMask.GetMask("Enemy"); 
            
            var hits = Physics2D.OverlapCircleAll(_attackPoint.Point.transform.position, range, mask);

            foreach (var hit in hits)
            {
                if (!hit.isTrigger && hit.TryGetComponent<IDamagable>(out var damagable))
                {
                    string[] args;

                    if (weaponItem != null)
                        args = weaponItem.ItemTags.ToArray();
                    
                    else args = new[] {""};

                    damagable.Damage(damage, args);
                    hitted.Add(damagable);
                }
            }

            return hitted.ToArray();
        }
    }
}