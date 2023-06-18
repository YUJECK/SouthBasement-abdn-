using SouthBasement.InternalAssets.Scripts.Helpers;
using UnityEngine;

namespace SouthBasement.AI
{
    [RequireComponent(typeof(TriggerCallback))]
    public sealed class AttackTrigger : MonoBehaviour
    {
        public bool CanAttack { get; private set; }

        private void Awake()
        {
            GetComponent<TriggerCallback>().OnTriggerEnter += (Collider2D other) => CanAttack = true;
            GetComponent<TriggerCallback>().OnTriggerExit += (Collider2D other) => CanAttack = false;
        }
    }
}