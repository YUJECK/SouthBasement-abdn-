using System;
using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement
{
    public class EffectsHandler : MonoBehaviour
    {
        private readonly List<Effect> _effects = new();

        private void OnDestroy()
        {
            foreach (var effect in _effects)
                effect.OnRemoved();
        }

        public void Add(Effect effect)
        {
            _effects.Add(effect);
            effect.OnAdded();
            StartCoroutine(effect.DieCoroutine((effect) => Remove(effect)));
        }
        public void Remove(Effect effect)
        {
            effect.OnRemoved();
            _effects.Remove(effect);
        }
        
        private void Update()
        {
            foreach (var effect in _effects)
                effect.OnUpdate();
        }
    }
}
