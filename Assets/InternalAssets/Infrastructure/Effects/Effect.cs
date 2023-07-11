using System;
using System.Collections;
using UnityEngine;

namespace SouthBasement
{
    public abstract class Effect
    {
        public float Duration { get; set; }
        protected readonly IDamagable Owner;

        protected Effect(IDamagable owner, float duration)
        {
            Owner = owner;
            Duration = duration;
        }

        public virtual IEnumerator DieCoroutine(Action<Effect> onDied)
        {
            yield return new WaitForSeconds(Duration);

            onDied?.Invoke(this);
        }

        public virtual void OnAdded(){}
        public virtual void OnUpdate(){}
        public virtual void OnRemoved(){}
    }
}
