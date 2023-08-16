using SouthBasement.Helpers;
using UnityEngine;
using System;

namespace SouthBasement
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class FocusedFireTargetChecker : MonoBehaviour
    {
        private Transform _target;

        public event Action<Transform> OnTargeted;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag(TagHelper.Player) && other.TryGetComponent(out IDamagable damagable))
            {
                _target = other.transform;
                OnTargeted?.Invoke(_target);
            }
        }
    }
}