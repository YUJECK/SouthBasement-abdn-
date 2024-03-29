﻿using System;
using System.Collections;
using SouthBasement.Helpers;
using SouthBasement.Items;
using UnityEngine;

namespace SouthBasement
{
    public sealed class BleedEffect : Effect
    {
        private readonly float _damageRate;
        private readonly int _damage;

        public BleedEffect(int damage, float damageRate, float duration, IDamagable damagable) : base(damagable, duration)
        {
            _damageRate = damageRate;
            _damage = damage;

            Icon = Resources.Load<Sprite>(ResourcesPathHelper.BleedIcon);
        }

        public override IEnumerator DieCoroutine(Action<Effect> onDied)
        {
            var startTime = Time.time;
            
            while (Time.time <= startTime + Duration)
            {
                yield return new WaitForSeconds(_damageRate);
                Owner.Damage(_damage, new [] { AttackTags.Effect});
            }
            
            onDied?.Invoke(this);
        }
    }
}