using System;
using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement
{
    public static class OverlapDecorator
    {
        private static Collider2D[] _results = new Collider2D[20];

        public static void DoFor<TComponent>(Vector2 position, float radius, int layerMask, Action<List<TComponent>> callback,
            bool triggersInclude = false)

        {
            var size = Physics2D.OverlapCircleNonAlloc(position, radius, _results, layerMask);
            
            var result = new List<TComponent>();
            
            for(int i = 0; i < size; i++)
            {
                if ((!_results[i].isTrigger || triggersInclude) && _results[i].TryGetComponent<TComponent>(out var component))
                    result.Add(component);
            }
            
            callback?.Invoke(result);
        }
    }
}