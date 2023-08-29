using System.Collections.Generic;
using SouthBasement.Characters.Components;
using SouthBasement.Characters.Rat;
using SouthBasement.Effects;
using SouthBasement.Weapons;
using UnityEngine;
using Zenject;

namespace SouthBasement.Characters
{
    public sealed class BaseRatAttacker : CharacterComponent<RatCharacter>, IAttacker
    {
        private readonly RatAttackerConfig _attackerConfig;
        private HitEffectSpawner _hitEffectSpawner;

        [Inject]
        private void Construct(HitEffectSpawner hitEffectSpawner)
        {
            _hitEffectSpawner = hitEffectSpawner;
        }
        
        public BaseRatAttacker(RatAttackerConfig attackerConfig)
        {
            _attackerConfig = attackerConfig;
        }

        public IDamagable[] Attack(CombatStats combatStats)
        {
            List<IDamagable> hitted = new(); 
            _attackerConfig.AttackPoint.Stop(combatStats.Multiplied.AttackRate - 0.05f);

            var mask = LayerMask.GetMask("Enemy"); 
            
            var hits = Physics2D.OverlapCircleAll(_attackerConfig.AttackPoint.Point.transform.position, combatStats.Multiplied.AttackRange, mask);

            foreach (var hit in hits)
            {
                if (!hit.isTrigger && hit.TryGetComponent<IDamagable>(out var damagable))
                {
                    var hitPos = (Vector2)hit.transform.position - hit.offset;

                    _hitEffectSpawner.Spawn(hitPos);
                    
                    damagable.Damage(combatStats.Multiplied.Damage, combatStats.AttackTags.ToArray());
                    hitted.Add(damagable);
                }
            }

            return hitted.ToArray();
        }

        public IDamagable[] Attack(int damage, float culldown, float range, string[] args)
        {
            throw new System.NotImplementedException();
        }
    }
}