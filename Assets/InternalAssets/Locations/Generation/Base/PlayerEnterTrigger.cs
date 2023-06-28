using System;
using SouthBasement.Characters;
using SouthBasement.Helpers;
using UnityEngine;

namespace SouthBasement.Generation
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerEnterTrigger : MonoBehaviour
    {
        [field: SerializeField] public bool MultiEntering { get; private set; } = false;
        
        public event Action<Character> OnEntered;
        public event Action<Character> OnExit;

        public bool Entered { get; private set; } = false;

        private void Awake()
        {
            var collider = GetComponent<Collider2D>();

            if (!collider.isTrigger)
                collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!Entered && other.CompareTag(TagHelper.Player))
            {
               OnEntered?.Invoke(other.GetComponent<Character>());
               
               if(!MultiEntering) Entered = true;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagHelper.Player))
            {
                OnExit?.Invoke(other.GetComponent<Character>());
            }
        }
    }
}
