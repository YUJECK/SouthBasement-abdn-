using System;
using System.Collections;
using System.Collections.Generic;
using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement.BaseScripts.Tests
{
    public sealed class ExtraDamageEffect : Effect
    {
        private readonly float _damageRate;
        private readonly int _damage;

        public ExtraDamageEffect(int damage, float damageRate, float duration, IDamagable damagable) : base(damagable, duration)
        {
            _damageRate = damageRate;
            _damage = damage;
        }

        public override IEnumerator DieCoroutine(Action<Effect> onDied)
        {
            var startTime = Time.time;
            
            while (Time.time <= startTime + Duration)
            {
                yield return new WaitForSeconds(_damageRate);
                Owner.Damage(_damage, new [] {AttackTags.Effect});
            }
            
            onDied?.Invoke(this);
        }
    }
}