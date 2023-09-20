using System.Collections.Generic;
using SouthBasement.Items;
using SouthBasement.Weapons;
using UnityEngine;

namespace SouthBasement
{
    public sealed class CarrotDroneBomb : CarrotDroneComponent
    {
        public GameObject boomEffect;

        public void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                CombatStats combatStats = new()
                {
                    Damage = CarrotDroneConfig.damage,
                    AttackTags = new List<AttackTags>(CarrotDroneConfig.attackTags)
                };
                
                damagable.Damage(combatStats.Multiplied.Damage, CarrotDroneConfig.attackTags);
                Explode();
            }
        }

        public void Explode()
        {
            Instantiate(boomEffect, transform.position, transform.rotation);
            Destroy(transform.parent.gameObject);
        }
    }
}