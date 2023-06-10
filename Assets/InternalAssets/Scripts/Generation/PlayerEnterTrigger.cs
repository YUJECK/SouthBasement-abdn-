using System;
using TheRat.Helpers;
using TheRat.Player;
using UnityEngine;

namespace TheRat.Generation
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class PlayerEnterTrigger : MonoBehaviour
    {
        public event Action<Character> OnEntered; 

        private void Start()
        {
            var collider = GetComponent<Collider2D>();

            if (!collider.isTrigger)
                collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagHelper.Player))
               OnEntered?.Invoke(other.GetComponent<Character>());
        }
    }
}
