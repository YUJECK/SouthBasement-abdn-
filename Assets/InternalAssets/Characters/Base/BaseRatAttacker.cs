using System.Collections.Generic;
using SouthBasement.Characters.Components;
using SouthBasement.Characters.Rat;
using UnityEngine;

namespace SouthBasement.Characters
{
    public sealed class BaseRatAttacker : CharacterComponent<RatCharacter>, IAttacker
    {
        private readonly RatAttackerConfig _attackerConfig;

        public BaseRatAttacker(RatAttackerConfig attackerConfig)
        {
            _attackerConfig = attackerConfig;
        }

        public IDamagable[] Attack(int damage, float culldown, float range, string[] args)
        {
            List<IDamagable> hitted = new(); 
            _attackerConfig.AttackPoint.Stop(culldown - 0.05f);

            var mask = LayerMask.GetMask("Enemy"); 
            
            var hits = Physics2D.OverlapCircleAll(_attackerConfig.AttackPoint.Point.transform.position, range, mask);

            foreach (var hit in hits)
            {
                if (!hit.isTrigger && hit.TryGetComponent<IDamagable>(out var damagable))
                {
                    var hitPos = (Vector2)hit.transform.position - hit.offset;

                    if(_attackerConfig.HitEffectPrefab != null)
                        GameObject.Instantiate(_attackerConfig.HitEffectPrefab, hitPos, Quaternion.identity);

                    damagable.Damage(damage, args);
                    hitted.Add(damagable);
                }
            }

            return hitted.ToArray();
        }
    }
}