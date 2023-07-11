using System;
using SouthBasement.Characters;
using SouthBasement.Characters.Base;
using SouthBasement.Helpers;
using UnityEngine;

namespace SouthBasement.Generation
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerEnterTrigger : MonoBehaviour
    {
        [field: SerializeField] public bool MultiEntering { get; private set; } = false;

        public bool CurrentOnTrigger { get; private set; }

        public event Action<CharacterGameObject> OnEntered;
        public event Action<CharacterGameObject> OnExit;

        public bool Entered { get; private set; } = false;

        private void Awake()
        {
            var collider = GetComponent<Collider2D>();

            if (!collider.isTrigger)
                collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((!Entered || MultiEntering) && other.CompareTag(TagHelper.Player))
            {
                CurrentOnTrigger = true;
                Entered = true;
                
                OnEntered?.Invoke(other.GetComponent<CharacterGameObject>());
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(TagHelper.Player) && CurrentOnTrigger)
            {
                CurrentOnTrigger = false;

                OnExit?.Invoke(other.GetComponent<CharacterGameObject>());
            }
        }
    }
}
