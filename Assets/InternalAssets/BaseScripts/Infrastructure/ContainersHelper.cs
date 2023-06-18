using UnityEngine;

namespace SouthBasement.Helpers
{
    [System.Serializable]
    public sealed class ContainersHelper
    {
        [field: SerializeField] public Transform RoomsContainer { get; private set; }
    }
}