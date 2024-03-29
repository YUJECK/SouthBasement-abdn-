﻿using SouthBasement.Characters.Components;
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

        public AttackResult Attack(CombatStats combatStats)
        {
            _attackerConfig.AttackPoint.Stop(combatStats.Multiplied.AttackRate - 0.05f);

            var mask = LayerMask.GetMask("Enemy"); 
            
            var results = Physics2D.OverlapCircleAll(_attackerConfig.AttackPoint.Point.transform.position, combatStats.Multiplied.AttackRange, mask);

            AttackResult attackResult = new(results);

            foreach (var hit in attackResult.ColliderHits)
            {
                var hitPos = (Vector2)hit.transform.position - hit.offset;
                _hitEffectSpawner.Spawn(hitPos);
            }
            
            foreach (var hit in attackResult.DamagedHits)
                hit.Damage(combatStats.Multiplied.Damage, combatStats.AttackTags.ToArray());

            return attackResult;
        }
    }
}