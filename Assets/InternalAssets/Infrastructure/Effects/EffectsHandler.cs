using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement
{
    public class EffectsHandler : MonoBehaviour
    {
        public bool Blocked { get; set; }
        
        private readonly List<Effect> _effects = new();
        private SpriteRenderer _effectIcon;

        private void Awake()
        {
            _effectIcon = transform.Find("EffectsHandlerIcon").GetComponent<SpriteRenderer>();
            DisableEffectIcon();
        }

        private void OnDestroy()
        {
            foreach (var effect in _effects)
                effect.OnRemoved();
        }

        private void Update()
        {
            foreach (var effect in _effects)
                effect.OnUpdate();
        }

        public void Add(Effect effect)
        {
            if (Blocked) return;
            
            _effects.Add(effect);
            effect.OnAdded();
            StartCoroutine(effect.DieCoroutine(Remove));

            EnableEffectIcon(effect);
        }

        public void Remove(Effect effect)
        {
            effect.OnRemoved();
            _effects.Remove(effect);

            DisableEffectIcon();
        }

        private void EnableEffectIcon(Effect effect)
        {
            if (effect.Icon != null)
            {
                _effectIcon.color = Color.white;
                _effectIcon.sprite = effect.Icon;
            }
        }

        private void DisableEffectIcon()
            => _effectIcon.color = Color.clear;
    }
}
