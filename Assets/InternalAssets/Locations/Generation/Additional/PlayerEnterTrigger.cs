using System;
using SouthBasement.Characters;
using SouthBasement.Helpers;
using UnityEngine;

namespace SouthBasement.Generation
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerEnterTrigger : MonoBehaviour
    {
        public event Action<Character> OnEntered;

        public bool Enabled { get; set; } = true;

        private void Awake()
        {
            var collider = GetComponent<Collider2D>();

            if (!collider.isTrigger)
                collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Enabled && other.CompareTag(TagHelper.Player))
            {
               OnEntered?.Invoke(other.GetComponent<Character>());
               Enabled = false;
            }
        }
    }
}
