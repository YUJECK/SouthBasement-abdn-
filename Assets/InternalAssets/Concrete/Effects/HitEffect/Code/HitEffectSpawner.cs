using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement.Effects
{
    public sealed class HitEffectSpawner
    {
        private readonly HitEffect _hitEffectPrefab;
        private readonly List<HitEffect> _effectsPool = new();

        private const int PoolSize = 10;
        
        public HitEffectSpawner(HitEffect hitEffectPrefab)
        {
            _hitEffectPrefab = hitEffectPrefab;

            CreatePool();
        }

        private void CreatePool()
        {
            for (int i = 0; i < PoolSize; i++)
            {
                var effect = GameObject.Instantiate(_hitEffectPrefab);
                _effectsPool.Add(effect);
                effect.gameObject.SetActive(false);
                GameObject.DontDestroyOnLoad(effect);
            }
        }

        public void Spawn(Vector2 position)
        {
            var effect = GetFree();

            effect.transform.position = position;
            effect.gameObject.SetActive(true);
        }

        private HitEffect GetFree()
        {
            foreach (var effect in _effectsPool)
            {
                if (!effect.gameObject.activeSelf)
                    return effect;
            }

            Debug.LogError("Pool is small. There isnt free objects");
            return null;
        }
    }
}