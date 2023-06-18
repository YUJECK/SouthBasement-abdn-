using System;
using System.Collections.Generic;
using UnityEngine;

namespace SouthBasement.InternalAssets.Scripts.Helpers
{
    public sealed class TriggerCallback : MonoBehaviour
    {
        [SerializeField] private List<string> tagList;

        public event Action<Collider2D> OnTriggerEnter;
        public event Action<Collider2D> OnTriggerExit;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(tagList.Contains(other.tag))
                OnTriggerEnter?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if(tagList.Contains(other.tag))
                OnTriggerExit?.Invoke(other);
        }
    }
}